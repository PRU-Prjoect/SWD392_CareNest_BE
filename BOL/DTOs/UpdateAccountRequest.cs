using BOL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class UpdateAccountRequest
    {
        public string? username {  get; set; }
        [EmailAddress]
        public string? email { get; set; }
        public Role? role { get; set; }
    }
}
