using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Room : BaseEntity
    {
        [Key]
        public Guid id { get; set; } 
        public int? room_number { get; set; }
        public int? room_type { get; set; }
        public int? max_capacity { get; set; }
        public double? daily_price { get; set; }
        public bool is_available { get; set; }
        public string? amendities { get; set; }
        public int? star { get; set; }
        [ForeignKey("hotel")]
        public Guid hotel_id { get; set; }

        public Hotel hotel { get; set; }
        public IEnumerable<Room_Booking>? room_booking { get; set; }
        public IEnumerable<Pet_Service_Room>? service { get; set; }


    }
}
