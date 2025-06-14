using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class ServiceDTO
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string name { get; set; }
        public bool is_active { get; set; }
        [ForeignKey("shop")]
        public Guid shop_id { get; set; }
        public string description { get; set; }
        public float discount_percent { get; set; }
        public double Price { get; set; }
        public int limit_per_hour { get; set; }
        //[ForeignKey("pet_type")]
        //public Guid pet_type_id { get; set; } 
        public int duration_type { get; set; }
        public float Star { get; set; }
        public int purchases { get; set; }
        [ForeignKey("service_type")]
        public Guid service_type_id { get; set; }
    }
}
