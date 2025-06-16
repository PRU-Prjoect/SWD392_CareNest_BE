using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Notification : BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        [ForeignKey("account")]
        public Guid receiver_id { get; set; }
        public string? description { get; set; }
        public bool is_read { get; set; }


        public Account account { get; set; }


    }
}
