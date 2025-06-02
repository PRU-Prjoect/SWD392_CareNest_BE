using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class RoomDTO
    {
        [Required]
        public Guid id { get; set; } = Guid.NewGuid();
        public string? name { get; set; }
        public string? description { get; set; }
        public double? price { get; set; }
        public Guid service_type_id { get; set; }
        public Guid shop_id { get; set; }
        public Guid pet_type_id { get; set; }
    }
}
