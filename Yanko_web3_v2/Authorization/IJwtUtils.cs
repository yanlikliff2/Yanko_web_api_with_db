using Yanko_web3_v2.Entities;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(UserTable account);
        public int? ValidateJwtToken(string token);
        public Task<RefreshToken> GenerateRefreshToken(string ipAddress);
    }
}
