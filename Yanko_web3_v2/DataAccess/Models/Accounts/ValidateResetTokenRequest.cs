using System.ComponentModel.DataAnnotations;

namespace Yanko_web3_v2.DataAccess.Models.Accounts
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
