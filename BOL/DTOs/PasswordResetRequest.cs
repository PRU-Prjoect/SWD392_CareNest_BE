using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class PasswordResetRequest
    {
        [Required]
        public string password { get; set; }
    }
}
