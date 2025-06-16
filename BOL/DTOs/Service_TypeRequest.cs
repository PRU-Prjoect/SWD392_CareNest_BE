using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class Service_TypeRequest
    {
        [Required]
        public Guid id { get; set; } = Guid.NewGuid();
        [Required]
        public string? name { get; set; }
        public string? description { get; set; }
        public IFormFile? img { get; set; }
        public bool is_public { get; set; }
    }
}
