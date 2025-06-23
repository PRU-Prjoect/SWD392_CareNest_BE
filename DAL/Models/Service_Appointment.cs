using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Service_Appointment : BaseEntity
    {
        [Key]
        public Guid id { get; set; }
        [ForeignKey("service")]
        public Guid service_id { get; set; }
        [ForeignKey("appointment")]
        public Guid appointment_id { get; set; }
        [ForeignKey("rating")]
        public Guid? rating_id { get; set; }

        public Service service { get; set; }
        public Appointments appointment { get; set; }
        public Rating? rating { get; set; }

    }
}
