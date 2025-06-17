using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class Service_AppointmentDTO
    {
        [Required]
        public Guid id { get; set; }
        public Guid service_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public Guid appointment_id { get; set; }
        public Guid rating_id { get; set; }
        public Guid room_id { get; set; }
    }
}
