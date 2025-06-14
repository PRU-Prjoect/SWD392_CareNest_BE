using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BOL.DTOs;
using DAL.Models;

namespace BLL.Mapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountRequest>().ReverseMap();
            CreateMap<Account, AccountResponse>().ReverseMap();
            CreateMap<UpdateAccountRequest, Account>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Account, UpdateAccountRequest>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Pet_Type, Pet_TypeDTO>().ReverseMap();

            CreateMap<Service_Type, Service_TypeDTO>().ReverseMap();

            CreateMap<Shop, ShopRequest>().ReverseMap();
            CreateMap<Shop, ShopResponse>().ReverseMap();

            CreateMap<Staff, StaffDTO>().ReverseMap();
            CreateMap<Staff, StaffResponse>().ReverseMap();

            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CustomerResponse>().ReverseMap();

            CreateMap<Sub_Address, Sub_AddressDTO>().ReverseMap();

            CreateMap<Service, ServiceDTO>().ReverseMap();

            CreateMap<Appointments, AppointmentsDTO>().ReverseMap();

            CreateMap<Rating, RatingDTO>().ReverseMap();

            CreateMap<Service_Appointment, Service_AppointmentDTO>().ReverseMap();

            CreateMap<Room, RoomDTO>().ReverseMap();    

            CreateMap<Hotel, HotelDTO>().ReverseMap();

            CreateMap<Pet_Service_Room, Pet_Service_RoomRequest>().ReverseMap();
            CreateMap<Pet_Service_Room, Pet_Service_RoomResponse>().ReverseMap();

            CreateMap<Room_Booking, Room_BookingDTO>().ReverseMap();

        }
    }
}
