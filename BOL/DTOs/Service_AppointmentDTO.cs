using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class Service_AppointmentDTO
    {
        [Required]
        public Guid service_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_type { get; set; }
        public Guid appointment_id { get; set; }
        public Guid rating_id { get; set; }
        public Guid room_id { get; set; }
    }
}
