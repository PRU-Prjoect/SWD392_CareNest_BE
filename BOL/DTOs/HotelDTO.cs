﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.DTOs
{
    public class HotelDTO
    {
        //public Guid id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int total_room { get; set; }
        public int available_room { get; set; }
        public Guid shop_id { get; set; }
        public Guid sub_address_id { get; set; }
        public bool is_active { get; set; }
    }
}
