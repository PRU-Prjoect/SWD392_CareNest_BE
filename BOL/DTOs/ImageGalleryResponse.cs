using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class ImageGalleryResponse
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string? name { get; set; }
        public string? img_url { get; set; }
        public string? owner_id { get; set; }
    }
}
