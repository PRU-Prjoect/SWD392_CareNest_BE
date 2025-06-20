using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class HotelResponse
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int total_room { get; set; }
        public int available_room { get; set; }
        public Guid shop_id { get; set; }
        public Guid sub_address_id { get; set; }
        public bool is_active { get; set; }
    }
}
