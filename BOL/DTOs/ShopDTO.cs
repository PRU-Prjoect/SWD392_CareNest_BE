using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class ShopDTO
    {
        [Required]
        public Guid account_id { get; set; }
        [Required]
        public string? name { get; set; }
        public string? description { get; set; }
        public bool status { get; set; }
        public List<string>? working_day { get; set; }
    }
}
