using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class Sub_AddressRequest
    {
        [Required]
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid shop_id { get; set; }
        public int? phone { get; set; }
        public string address_name { get; set; }
        public bool is_default { get; set; }
    }
}
