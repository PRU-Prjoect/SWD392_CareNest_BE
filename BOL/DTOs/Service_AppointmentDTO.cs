using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BOL.DTOs
{
    public class Service_AppointmentDTO
    {
        [Required]
        public Guid id { get; set; }
        public Guid service_id { get; set; }
        public Guid appointment_id { get; set; }
        public Guid? rating_id { get; set; }
    }
}
