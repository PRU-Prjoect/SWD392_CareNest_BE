using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Pet_Service_Room : BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid owner_id { get; set; }
        [ForeignKey("pet_type")]
        public Guid pet_type_id { get; set; }
        public bool is_service { get; set; }
        [ForeignKey("service")]
        public Guid service_id { get; set; }
        [ForeignKey("room")]
        public Guid room_id { get; set; }

        public Room room { get; set; }
        public Service service { get; set; }
        public Pet_Type pet_type { get; set; }
    }
}
