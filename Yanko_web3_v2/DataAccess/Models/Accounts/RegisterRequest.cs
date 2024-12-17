﻿using System.ComponentModel.DataAnnotations;

namespace Yanko_web3_v2.DataAccess.Models.Accounts
{
    public class RegisterRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Middlename { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Range(typeof(bool), "true", "true")]
        public bool AcceptTerms { get; set; }
    }
}
