using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Shop : BaseEntity
    {
        [Key]
        [ForeignKey("account")]
        public Guid account_id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public bool status { get; set; }
        public List<string>? working_day { get; set; }


        public Account account { get; set; }
        public IEnumerable<Staff>? staff { get; set; }
        public IEnumerable<Sub_Address>? sub_address { get; set; }
        public IEnumerable<Service>? service { get; set; }
        public IEnumerable<Hotel>? hotel { get; set; }
        //public IEnumerable<Room>? room { get; set; }
    }
}
