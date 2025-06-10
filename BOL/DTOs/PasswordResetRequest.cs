using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class PasswordResetRequest
    {
        [Required]
        public string password { get; set; }
    }
}
