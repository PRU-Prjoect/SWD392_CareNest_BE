using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class Pet_TypeDTO
    {
        [Required]
        public Guid id { get; set; } 
        public string? name { get; set; }
    }
}
