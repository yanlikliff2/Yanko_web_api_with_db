using Yanko_web3_v2.Helpers;
using Microsoft.Extensions.Options;
using Yanko_web3_v2.Authorization;
using Yanko_web3_v2.Models;
using Yanko_web3_v2.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Org.BouncyCastle.Crypto;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Yanko_web3_v2.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;
        private readonly PractDbContext _practDbContext;
        public JwtUtils(PractDbContext practDbContext,IOptions<AppSettings> appSettings) {
            { _appSettings = appSettings.Value;
                _practDbContext = practDbContext;
            }
        }

        public string GenerateJwtToken(UserTable account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] { new Claim("id", account.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }

        public async Task<RefreshToken> GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedById = ipAddress
            };
            var tokenIsUnique = (await _practDbContext.UserTables.AnyAsync(a => a.RefreshTokens.Any(t => t.Token == refreshToken.Token)));
            if (!tokenIsUnique)
                return await GenerateRefreshToken(ipAddress);
            return refreshToken;
        }

        public int? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return accountId;
            }
            catch
            {
                return null;
            }
        }

        
    } 
}
