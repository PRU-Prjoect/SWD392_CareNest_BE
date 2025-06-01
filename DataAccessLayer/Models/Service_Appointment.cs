using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Service_Appointment : BaseEntity
    {
        [Key]
        [ForeignKey("service")]
        public Guid service_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_type { get; set; }
        [ForeignKey("appointment")]
        public Guid appointment_id { get; set; }
        [ForeignKey("rating")]
        public Guid rating_id { get; set; }
        [ForeignKey("room")]
        public Guid room_id { get; set; }
        

        public Room room { get; set; }
        public Service service { get; set; }
        public Appointments appointment { get; set; }
        public Rating rating { get; set; }

    }
}
