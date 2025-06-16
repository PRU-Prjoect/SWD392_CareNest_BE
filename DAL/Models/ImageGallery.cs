using System.ComponentModel.DataAnnotations;

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
