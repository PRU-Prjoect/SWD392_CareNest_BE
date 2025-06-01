using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
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
