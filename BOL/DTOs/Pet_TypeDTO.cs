using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class Pet_TypeDTO
    {
        [Required]
        public Guid id { get; set; } = Guid.NewGuid();
        [Required]
        public string? name { get; set; }
    }
}
