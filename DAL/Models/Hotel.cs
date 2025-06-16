using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Hotel : BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public string? name { get; set; }
        public string? description { get; set; }
        public int? total_room { get; set; }
        public int? available_room { get; set; }
        [ForeignKey("shop")]
        public Guid shop_id { get; set; }
        [ForeignKey("sub_address")]
        public Guid? sub_address_id { get; set; }
        public bool is_active { get; set; }

        public Shop shop { get; set; }
        public Sub_Address? sub_address { get; set; }
        public IEnumerable<Room>? room { get; set; }
    }
}
