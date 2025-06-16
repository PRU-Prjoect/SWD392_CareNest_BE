using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.DTOs
{
    public class Room_BookingDTO
    {
        public Guid id { get; set; } = Guid.NewGuid();
        [ForeignKey("room")]
        public Guid room_detail_id { get; set; }
        [ForeignKey("customer")]
        public Guid customer_id { get; set; }
        public DateTime check_in_date { get; set; }
        public DateTime check_out_date { get; set; }
        public int total_night { get; set; }
        public int total_amount { get; set; }
        public DateTime feeding_schedule { get; set; }
        public DateTime medication_schedule { get; set; }
        public bool status { get; set; }
    }
}
