using System.ComponentModel.DataAnnotations;

namespace Yanko_web3_v2.DataAccess.Models.Accounts
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
