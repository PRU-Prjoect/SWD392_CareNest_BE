using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Room_Booking : BaseEntity
    {
        [Key]
        public Guid id { get; set; } 
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


        public Room room { get; set; }
        public Customer customer { get; set; }
    }
}
