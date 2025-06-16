using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class SendOtpRequest
    {
        [Required]
        public string email { get; set; }
    }
}
