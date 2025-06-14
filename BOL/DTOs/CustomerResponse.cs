using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class CustomerResponse
    {
        public Guid account_id { get; set; }
        public string? full_name { get; set; }
        public string? gender { get; set; }
        public DateTime? birthday { get; set; }
        public AccountResponse? account { get; set; }
    }
}
