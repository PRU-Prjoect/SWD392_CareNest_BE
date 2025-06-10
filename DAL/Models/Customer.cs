using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
