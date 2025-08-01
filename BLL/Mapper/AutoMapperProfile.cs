﻿using AutoMapper;
using BOL;
using BOL.DTOs;
using DAL.Models;

namespace BLL.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountRequest>().ReverseMap();
            CreateMap<Account, AccountResponse>().ReverseMap();
            CreateMap<UpdateAccountRequest, Account>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Account, UpdateAccountRequest>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Pet_Type, Pet_TypeDTO>().ReverseMap();

            CreateMap<Service_Type, Service_TypeRequest>().ReverseMap();
            CreateMap<Service_Type, Service_TypeResponse>().ReverseMap();

            CreateMap<Shop, ShopRequest>().ReverseMap();
            CreateMap<Shop, ShopResponse>().ReverseMap();

            CreateMap<Staff, StaffDTO>().ReverseMap();
            CreateMap<Staff, StaffResponse>().ReverseMap();

            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CustomerResponse>().ReverseMap();

            CreateMap<Sub_Address, Sub_AddressDTO>().ReverseMap();
            CreateMap<Sub_Address, Sub_AddressResponse>().ReverseMap();

            CreateMap<Service, ServiceDTO>().ReverseMap();
            CreateMap<Service, ServiceResponse>().ReverseMap();

            CreateMap<Appointments, AppointmentsDTO>().ReverseMap();

            CreateMap<Rating, RatingDTO>().ReverseMap();

            CreateMap<Service_Appointment, Service_AppointmentDTO>().ReverseMap();

            CreateMap<Room, RoomDTO>().ReverseMap();

            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, HotelResponse>().ReverseMap();

            CreateMap<Pet_Service_Room, Pet_Service_RoomRequest>().ReverseMap();
            CreateMap<Pet_Service_Room, Pet_Service_RoomResponse>().ReverseMap();

            CreateMap<Room_Booking, Room_BookingDTO>().ReverseMap();

            CreateMap<Notification, NotificationDTO>().ReverseMap();

            CreateMap<ImageGallery, ImageGalleryResponse>().ReverseMap();
            CreateMap<ImageGallery, ImageGalleryRequest>().ReverseMap();

        }
    }
}
