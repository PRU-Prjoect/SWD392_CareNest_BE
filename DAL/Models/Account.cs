using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models.Enums;

namespace DAL.Models
{
    public class Account: BaseEntity
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [EmailAddress]
        [Required]
        public string email { get; set; }
        public Role role  { get; set; }
        public bool is_active { get; set; }
        public string? img_url { get; set; }
        public string? otp {  get; set; }
        public DateTime? otpExpired { get; set; }


        public Shop? shop { get; set; }
        public Staff? staff { get; set; }
        public Customer? customer { get; set; }
        public IEnumerable<Notification>? notification { get; set; }


    }
}
