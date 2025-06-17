using BOL.Enums;
using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class UpdateAccountRequest
    {
        public string? username { get; set; }
        [EmailAddress]
        public string? email { get; set; }
        public Role? role { get; set; }
        public string? BANK_ACCOUNT_NO { get; set; }
        public string? BANK_ACCOUNT_NAME { get; set; }
        public string? BANK_ID { get; set; }
    }
}
