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
        [Required]
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid customer_id { get; set; }
        public string? location_type { get; set; }
        public bool? status { get; set; }
        public string? notes { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_type { get; set; }
    }
}
