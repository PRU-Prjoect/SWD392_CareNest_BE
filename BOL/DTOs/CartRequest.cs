using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class CartRequest
    {
        [Required]
        public Guid customer_id {  get; set; }
        [Required]
        public Guid service_id {  get; set; }
    }
}
