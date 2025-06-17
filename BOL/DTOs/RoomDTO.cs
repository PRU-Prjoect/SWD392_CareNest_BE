using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.DTOs
{
    public class RoomDTO
    {
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

    }
}
