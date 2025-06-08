using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BOL.DTOs
{
    public class AccountDTO
    {
        [Required]
        public string username {  get; set; }
        [Required]
        public string password {  get; set; }
        [Required]
        [EmailAddress]
        public string email {  get; set; }
        public string? img_url { get; set; }
        public string? img_url_id { get; set; }
        public IFormFile? img { get; set; }

    }
}
