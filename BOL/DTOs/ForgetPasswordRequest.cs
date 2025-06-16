using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class ForgetPasswordRequest
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
