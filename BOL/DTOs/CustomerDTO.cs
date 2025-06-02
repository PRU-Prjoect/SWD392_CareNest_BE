using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class CustomerDTO
    {
        [Required]
        public Guid account_id { get; set; }
        public string? full_name { get; set; }
        public string? gender { get; set; }
        public DateTime? birthday { get; set; }
    }
}
