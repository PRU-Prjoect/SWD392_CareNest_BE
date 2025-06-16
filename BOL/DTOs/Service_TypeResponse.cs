using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class Service_TypeResponse
    {
        [Required]
        public Guid id { get; set; } = Guid.NewGuid();
        [Required]
        public string? name { get; set; }
        public string? description { get; set; }
        public string? img_url { get; set; }
        public bool is_public { get; set; }
    }
}
