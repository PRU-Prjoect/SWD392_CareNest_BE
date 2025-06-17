using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Sub_Address : BaseEntity
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string name { get; set; }
        [ForeignKey("shop")]
        public Guid shop_id { get; set; }
        public int? phone { get; set; }
        [Required]
        public string address_name { get; set; }
        public bool is_default { get; set; }


        public Shop shop { get; set; }
    }
}
