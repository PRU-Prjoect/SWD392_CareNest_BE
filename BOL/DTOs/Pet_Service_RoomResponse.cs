using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class Pet_Service_RoomResponse
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid owner_id { get; set; }
        [ForeignKey("pet_type")]
        public Guid pet_type_id { get; set; }
        public bool is_service { get; set; }
        [ForeignKey("service")]
        public Guid service_id { get; set; }
        [ForeignKey("room")]
        public Guid room_id { get; set; }
    }
}
