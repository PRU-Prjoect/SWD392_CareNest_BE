﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace BOL.DTOs
{
    public class AccountRequest
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        public string? BANK_ACCOUNT_NO { get; set; }
        public string? BANK_ACCOUNT_NAME { get; set; }
        public string? BANK_ID { get; set; }
        public IFormFile? img { get; set; }

    }
}
