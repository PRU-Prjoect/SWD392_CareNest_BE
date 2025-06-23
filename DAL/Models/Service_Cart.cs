using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Service_Cart : BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        [ForeignKey("cart")]
        public Guid cart_id { get; set; }
        [ForeignKey("service")]
        public Guid service_id { get; set; }


        public Cart cart { get; set; }
        public Service service { get; set; }
    }
}
