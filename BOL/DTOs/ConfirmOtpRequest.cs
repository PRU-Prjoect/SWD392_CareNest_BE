using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class ConfirmOtpRequest
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string otp { get; set; }
    }
}
