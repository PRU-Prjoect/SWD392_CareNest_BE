using System.ComponentModel.DataAnnotations;

namespace BOL.DTOs
{
    public class LoginReq
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
