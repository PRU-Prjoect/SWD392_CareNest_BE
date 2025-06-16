using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

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
