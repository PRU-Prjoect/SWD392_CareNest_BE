using BOL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class AppointmentsDTO
    {
        public Guid customer_id { get; set; }
        public string location_type { get; set; }
        public AppointmentStatus status { get; set; }
        public string? notes { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
    }
}
