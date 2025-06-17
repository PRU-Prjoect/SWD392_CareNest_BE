using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class RatingDTO
    {

        public Guid customer_id { get; set; }
        public float? star { get; set; }
        public string? comment { get; set; }
    }
}
