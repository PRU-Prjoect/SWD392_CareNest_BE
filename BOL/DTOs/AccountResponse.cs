using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class AccountResponse
    {
        public string Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string? img_url { get; set; }
        public string? img_url_id { get; set; }
    }
}
