using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Pet_Type : BaseEntity
    {
        [Key]
        public Guid id { get; set; } 
        public string? name { get; set; }

    }
}
