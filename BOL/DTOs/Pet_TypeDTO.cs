using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class Pet_TypeDTO
    {
        [Required]
        public Guid id { get; set; } = Guid.NewGuid();
        [Required]
        public string? name { get; set; }
    }
}
