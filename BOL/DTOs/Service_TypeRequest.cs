using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class Service_TypeRequest
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public IFormFile? img { get; set; }
        public bool is_public { get; set; }
    }
}
