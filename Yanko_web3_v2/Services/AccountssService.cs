using BlazorClient.Models.Accounts;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Security.Cryptography;
using Yanko_web3_v2.Authorization;
using Yanko_web3_v2.DataAccess.Models.Accounts;
using Yanko_web3_v2.Entities;
using Yanko_web3_v2.Helpers;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Sevices
{
    public class AccountssService : IAccountService
    {
        private readonly PractDbContext _user;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public AccountssService(
            IJwtUtils jwtUtils, 
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IEmailService emailService,
            PractDbContext user
            ) 
        {
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _emailService = emailService;
            _user = user;
        }

        private void removeOldRefreshTokens(UserTable account)
        {
            account.RefreshTokens.RemoveAll(x => 
            !x.IsActive &&
            x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = await _user.UserTables.Include(x => x.RefreshTokens).AsNoTracking().FirstOrDefaultAsync(x => x.Email == model.Email);

            if (account == null || !account.IsVerified || !BCrypt.Net.BCrypt.Verify(model.Password, account.Password))
                throw new AppException("Email or password is incorrect");
            
            var jwtToken = _jwtUtils.GenerateJwtToken(account);
            var refreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);

            removeOldRefreshTokens(account);

            _user.UserTables.Update(account);
            await _user.SaveChangesAsync();

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task<AccountResponse> Create(CreateRequest model)
        {
            var existingUserCount = await _user.UserTables.Where(x => x.Email == model.Email).CountAsync();

            if (existingUserCount > 0)
            {
                throw new AppException($"Email '{model.Email}' is already registered");
            }


            var account = _mapper.Map<UserTable>(model);
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;

            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _user.UserTables.Update(account);
            await _user.SaveChangesAsync();

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task Delete(int id)
        {
            var account = await getAccount(id);
            _user.UserTables.Remove (account);
            await _user.SaveChangesAsync();
        }

        private async Task<string> generateResetToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            var tokenIsUnique = await _user.UserTables.AnyAsync(x => x.ResetToken == token);
            if (!tokenIsUnique) 
            {
                return await generateResetToken();
            }
            return token;

        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = _user.UserTables.Where(x => x.Email == model.Email).FirstOrDefault();
            if (account == null) return; 
            account.ResetToken = await generateResetToken();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
            _user.UserTables.Update(account);
            await _user.SaveChangesAsync();
        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            //var accounts = await _user.UserTables.FindAll();
            //return _mapper.Map<IList<AccountResponse>>(accounts);
            List<UserTable> accounts = await _user.UserTables.ToListAsync();
            return _mapper.Map<IList<AccountResponse>>(accounts);
        }
        private async Task<UserTable> getAccountByRefreshToken(string token)
        {
            var account = ( _user.UserTables.Where(u => u.RefreshTokens.Any(t => t.Token == token))).SingleOrDefault();
            if (account == null) throw new AppException("Invalid token");
            return account;
        }
        private async Task<RefreshToken> rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Replased by new token");
            return newRefreshToken;
        }
        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevoketById = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplasedByToken = replacedByToken; 
        }
        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, UserTable account, string ipAddress, string reason) 
        {
            if (!string.IsNullOrEmpty(refreshToken.ReplasedByToken)) 
            { 
                var childToken = account.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplasedByToken);
                if (childToken.IsActive)
                {
                    revokeRefreshToken(childToken, ipAddress, reason);
                }
                else 
                {
                    revokeDescendantRefreshTokens(childToken, account, ipAddress, reason);
                }
            }
        }
        public async Task<AccountResponse> GetById(int id)
        {
            var account = await getAccount(id);
            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var account = await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsActive) { throw new AppException("Invalid token"); }

            var newRefreshToken = await rotateRefreshToken(refreshToken, ipAddress);
            account.RefreshTokens.Add( newRefreshToken);

            removeOldRefreshTokens(account);

            _user.UserTables.Update(account);
            await _user.SaveChangesAsync();

            var jwtToken = _jwtUtils.GenerateJwtToken(account);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            return response;
        }
        private async Task<string> generateVertificationToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            var tokenIsUnique = (await _user.UserTables.AnyAsync(x => x.ResetToken == token));
            if (tokenIsUnique) { return await generateVertificationToken(); }
            return token;
        }
        public async Task Register(RegisterRequest model, string origin)
        {
            string email = model.Email;
            if (await _user.UserTables.AnyAsync(x => x.Email == email)) 
                return;
            var account = _mapper.Map<UserTable>(model);

            var isFirstAccount = (await _user.UserTables.AnyAsync(x => x.Email == model.Email));
            account.Username = model.Login;
            account.Role = isFirstAccount ? Role.Admin : Role.User;
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;
            account.VerificationToken = await generateVertificationToken();

            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            await _user.UserTables.AddAsync(account);
            await _user.SaveChangesAsync();
        }
        private async Task<UserTable> getAccountByResetToken(string token)
        {
            var account = await _user.UserTables.Where(x => x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow).SingleOrDefaultAsync();
            if (account == null) throw new AppException("Invalid token");
            return account;
        }

        public async Task ResetPassword(ResetPasswordRequest model)
        {
            var account = await getAccountByResetToken(model.Token);
            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            account.PassordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;
            _user.UserTables.Update(account);
            await _user.SaveChangesAsync();
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var account = await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive) throw new AppException("Invalid token");
            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _user.UserTables.Update(account);
            await _user.SaveChangesAsync();
        }

        private async Task<UserTable> getAccount(int id)
        {
            var account = (_user.UserTables.Where(x => x.UserId == id)).FirstOrDefault();
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        public async Task<AccountResponse> Update(int id, UpdateRequest model)
        {
            var account = await getAccount(id);

            if (await _user.UserTables.Where(x => x.Email == model.Email).CountAsync() > 0)
                throw new AppException( $"Email '{ model.Email } ' is already registered");

            if (!string.IsNullOrEmpty(model.Password))
                account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _mapper.Map(model, account);
            account.Updated = DateTime.UtcNow;
            _user.UserTables.Update(account);
            await _user.SaveChangesAsync();

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            await getAccountByResetToken(model.Token);
        }

        public async Task VerifyEmail(string token)
        {
            var account = _user.UserTables.Where(x => x.VerificationToken == token).FirstOrDefault();
            if (account == null) throw new AppException("Verification faled");
            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;
            _user.UserTables.Update(account);
            await _user.SaveChangesAsync();
        }
    }
}
