using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Room : BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public string? name { get; set; }
        public string? description { get; set; }
        public double? price { get; set; }
        [ForeignKey("service_type")]
        public Guid service_type_id { get; set; }
        [ForeignKey("shop")]
        public Guid shop_id { get; set; }
        [ForeignKey("pet_type")]
        public Guid pet_type_id { get; set; }
        

        public Shop shop { get; set; }
        public Pet_Type pet_type { get; set; }
        public Service_Type service_type { get; set; }
        public IEnumerable<Service_Appointment> service_appointment { get; set; }


    }
}
