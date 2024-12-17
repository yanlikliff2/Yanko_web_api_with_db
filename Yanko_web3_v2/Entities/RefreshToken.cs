using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public UserTable Account { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedById { get; set; }
        public DateTime? Revoked {  get; set; }
        public string? RevoketById { get; set;}
        public string? ReplasedById { get; set; }
        public string? ReasonRevoked {  get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsRevoked => Revoked != null;
        public bool IsActive => Revoked == null && !IsExpired;

    }
}
