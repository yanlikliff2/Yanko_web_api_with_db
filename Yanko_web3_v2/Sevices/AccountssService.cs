using BlazorClient.Models.Accounts;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Yanko_web3_v2.Authorization;
using Yanko_web3_v2.DataAccess.Models.Accounts;
using Yanko_web3_v2.Helpers;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Sevices
{
    public class AccountService : IAccountService
    {
        private readonly PractDbContext _user;
        /*private readonly PractDbContext _advertisement;
        private readonly PractDbContext _channel;
        private readonly PractDbContext _collection;
        private readonly PractDbContext _comment;
        private readonly PractDbContext _image;
        private readonly PractDbContext _object;
        private readonly PractDbContext _role;
        private readonly PractDbContext _subscriptions;
        private readonly PractDbContext _tag;*/
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public AccountService(
            IJwtUtils jwtUtils, 
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IEmailService emailService,
            PractDbContext user/*,
            PractDbContext advertisement,
            PractDbContext channel,
            PractDbContext collection,
            PractDbContext comment,
            PractDbContext image,
            PractDbContext obj,
            PractDbContext role,
            PractDbContext subscriptions,
            PractDbContext tag*/
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
            var account = await _user.UserTables.Include(x => x.ResetToken).AsNoTracking().FirstOrDefaultAsync(x => x.Email == model.Email);

            if (account == null || !account.IsVerified || !BCrypt.Net.BCrypt.Verify(model.Password, account.PasswordHash))
                throw new AppException("Email or password is incorrect");
            
            var jwtToken = _jwtUtils.GenerateJwtToken(account);
            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);

            removeOldRefreshTokens(account);

            await _repositoryWrapper.Users.Update(account);
            await _repositoryWrapper.Save();

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task<AccountResponse> Create(CreateRequest model)
        {
            if ((await _user.UserTables.FindByCondoition(x =>
            x.Email == model.Email)).Count > 0) throw new AppException(
            $'Email '{ model.Email } ' is already registered');

            var account = _mapper.Map<User>(model);
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;

            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task Delete(int id)
        {
            var account = await getAccount(id);
            await _repositoryWrapper.User.Delete(account);
            await _repositoryWrapper.Save();
        }

        private async Task<string> generateResetToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));


        }

        public Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {

        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            var accounts = await _repositoryWrapper.User.FindAll();
            return _mapper.Map<IList<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse> GetById(int id)
        {
            var account = await getAccount(id);
            return _mapper.Map<AccountResponse>(account);
        }

        public Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task Register(RegisterRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public Task ResetPassword(ResetPasswordRequest model)
        {
            throw new NotImplementedException();
        }

        public Task RevokeToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        private async Task<User> getAccount(int id)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(x => x.Id == id)).FirstOrDefault();
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        public async Task<AccountResponse> Update(int id, UpdateRequest model)
        {
            var account = await getAccount(id);

            if (account.Email != model.Email && (await _repositoryWrapper.User.FindByCondition(x => x.Email == model.Email)).Count > 0)
                throw new AppException( $'Email '{ model.Email } ' is already registered');

            if (!string.IsNullOrEmpty(model.Password))
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _mapper.Map(model, account);
            account.Updated = DateTime.UtcNow;
            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();

            return _mapper.Map<AccountResponse>(account);
        }

        public Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            throw new NotImplementedException();
        }

        public Task VerifyEmail(string token)
        {
            throw new NotImplementedException();
        }
    }
}
