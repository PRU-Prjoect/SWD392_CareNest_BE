using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace BOL.DTOs
{
    public class AccountRequest
    {
        [Required]
        public string username {  get; set; }
        [Required]
        public string password {  get; set; }
        [Required]
        [EmailAddress]
        public string email {  get; set; }
        public IFormFile? img { get; set; }

    }
}
