using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Rating : BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        [ForeignKey("customer")]
        public Guid customer_id { get; set; }
        public float? star { get; set; }
        public string? comment { get; set; }
        

        public Customer customer { get; set; }
        public Service_Appointment service_appointment { get; set; }
    }
}
