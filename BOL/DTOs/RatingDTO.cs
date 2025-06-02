using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class RatingDTO
    {
        [Required]
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid customer_id { get; set; }
        public float? star { get; set; }
        public string? comment { get; set; }
    }
}
