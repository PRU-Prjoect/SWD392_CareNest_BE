using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class RatingDTO
    {
        [Required]
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid customer_id { get; set; }
        public float? star { get; set; }
        public string? comment { get; set; }
    }
}
