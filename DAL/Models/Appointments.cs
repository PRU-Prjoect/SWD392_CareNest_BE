using BOL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Appointments : BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        [ForeignKey("customer")]
        public Guid customer_id { get; set; }
        public string location_type { get; set; }
        public AppointmentStatus status { get; set; }
        public string? notes { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }


        public Customer customer { get; set; }
        public IEnumerable<Service_Appointment> service_appointment { get; set; }
    }
}
