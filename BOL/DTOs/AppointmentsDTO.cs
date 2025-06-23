using BOL.Enums;

namespace BOL.DTOs
{
    public class AppointmentsDTO
    {
        public Guid id { get; set; } 
        public Guid customer_id { get; set; }
        public AppointmentStatus status { get; set; }
        public string? notes { get; set; }
        public DateTime start_time { get; set; }
    }
}
