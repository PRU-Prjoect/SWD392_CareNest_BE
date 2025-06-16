using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Pet_Type : BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public string? name { get; set; }

    }
}
