using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class StaffResponse
    {
        [Required]
        public Guid account_id { get; set; }
        [Required]
        public Guid shop_id { get; set; }
        public string? full_name { get; set; }
        public string? gender { get; set; }
        public string? position { get; set; }
        public DateTime? birthday { get; set; }
        public string? hired_at { get; set; }
        public string? shop_address_id { get; set; }

        public AccountResponse account { get; set; }
        public ShopResponse shop { get; set; }
    }
}
