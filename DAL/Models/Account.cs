using BOL.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Account : BaseEntity
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [EmailAddress]
        [Required]
        public string email { get; set; }
        public Role role { get; set; }
        public bool is_active { get; set; }
        public string? img_url { get; set; }
        public string? img_url_id { get; set; }
        public string? BANK_ACCOUNT_NO { get; set; }
        public string? BANK_ACCOUNT_NAME { get; set; }
        public string? BANK_ID { get; set; }
        public string? otp { get; set; }
        public DateTime? otpExpired { get; set; }


        public Shop? shop { get; set; }
        public Staff? staff { get; set; }
        public Customer? customer { get; set; }
        public IEnumerable<Notification>? notification { get; set; }


    }
}
