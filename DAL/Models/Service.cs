﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Service : BaseEntity
    {
        [Key]
        public Guid id { get; set; } 
        public string name { get; set; }
        public bool is_active { get; set; }
        [ForeignKey("shop")]
        public Guid shop_id { get; set; }
        public string description { get; set; }
        public float discount_percent { get; set; }
        public double Price { get; set; }
        public int limit_per_hour { get; set; }
        //[ForeignKey("pet_type")]
        //public Guid pet_type_id { get; set; } 
        public int duration_type { get; set; }
        public float Star { get; set; }
        public int purchases { get; set; }
        [ForeignKey("service_type")]
        public Guid service_type_id { get; set; }
        public string? img_url { get; set; }
        public string? img_url_id { get; set; }


        public Shop shop { get; set; }
        //public Pet_Type pet_type { get; set; }
        public Service_Type service_type { get; set; }
        public IEnumerable<Service_Appointment>? service_appointment { get; set; }
        public IEnumerable<Pet_Service_Room>? room { get; set; }
        public IEnumerable<Service_Cart>? service_Carts { get; set; }

        //public IEnumerable<ImageGallery> image_gallery { get; set; }
    }
}
