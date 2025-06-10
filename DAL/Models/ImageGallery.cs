using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ImageGallery : BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public string? name { get; set; }
        public string? img_url { get; set; }
        public string? owner_id { get; set; }
        //[ForeignKey("service")]
        //public Guid service_id { get; set; }
        

        //public Service service { get; set; }
    }
}
