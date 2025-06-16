using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        [ForeignKey("account")]
        public Guid account_id { get; set; }
        public string? full_name { get; set; }
        public string? gender { get; set; }
        public DateTime? birthday { get; set; }


        public Account account { get; set; }
        public IEnumerable<Rating>? rating { get; set; }
        public IEnumerable<Appointments>? appointment { get; set; }
        public IEnumerable<Room_Booking>? room_booking { get; set; }


    }
}
