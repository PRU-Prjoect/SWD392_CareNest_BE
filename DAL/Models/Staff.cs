﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Staff : BaseEntity
    {
        [Key]
        [ForeignKey("account")]
        public Guid account_id { get; set; }
        [ForeignKey("shop")]
        public Guid shop_id { get; set; }
        public string? full_name { get; set; }
        public string? gender { get; set; }
        public string? position { get; set; }
        public DateTime? birthday { get; set; }
        public string? hired_at { get; set; }
        public string? shop_address_id { get; set; }


        public Account account { get; set; }
        public Shop shop { get; set; }

    }
}
