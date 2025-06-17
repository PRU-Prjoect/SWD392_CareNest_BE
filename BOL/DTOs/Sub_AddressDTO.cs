using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class Sub_AddressDTO
    {
        [Required]
        public Guid id { get; set; } 
        public string name { get; set; }
        public Guid shop_id { get; set; }
        public int? phone { get; set; }
        public string address_name { get; set; }
        public bool is_default { get; set; }
    }
}
