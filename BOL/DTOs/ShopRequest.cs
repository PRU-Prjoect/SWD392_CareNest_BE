using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class ShopRequest
    {
        [Required]
        public Guid account_id { get; set; }
        [Required]
        public string? name { get; set; }
        public string? description { get; set; }
        public string? phone { get; set; }
        public bool status { get; set; }
        public List<string>? working_day { get; set; }
    }
}
