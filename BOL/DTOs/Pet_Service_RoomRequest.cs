using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.DTOs
{
    public class Pet_Service_RoomRequest
    {
        public Guid id { get; set; } 
        [ForeignKey("pet_type")]
        public Guid pet_type_id { get; set; }
        public bool is_service { get; set; }
        [ForeignKey("service")]
        public Guid service_id { get; set; }
        [ForeignKey("room")]
        public Guid room_id { get; set; }
    }
}
