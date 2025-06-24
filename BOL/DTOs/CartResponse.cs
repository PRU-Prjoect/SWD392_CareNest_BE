using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class CartResponse
    {
        public Guid cart_id { get; set; } 
        public Guid customer_id { get; set; }
        public double total { get; set; }
        public List<ServiceDTO>? services { get; set; }
    }
}
