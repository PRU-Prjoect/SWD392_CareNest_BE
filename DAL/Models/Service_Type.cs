using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Service_Type : BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public string? name { get; set; }
        public string? description { get; set; }
        public string? img_url { get; set; }
        public bool is_public { get; set; }


        public IEnumerable<Service>? service { get; set; }
    }
}
