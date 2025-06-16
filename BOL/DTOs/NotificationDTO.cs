using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class NotificationDTO
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid receiver_id { get; set; }
        public string? description { get; set; }
        public bool is_read { get; set; }
    }
}
