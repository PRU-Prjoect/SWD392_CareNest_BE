using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Cart : BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        [ForeignKey("customer")]
        public Guid customer_id { get; set; }
        public double total {  get; set; }


        public Customer customer { get; set; }
        public List<Service_Cart>? service_Carts { get; set; }

    }
}
