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
            CreateMap<Account, AccountDTO>().ReverseMap();

            CreateMap<Pet_Type, Pet_TypeDTO>().ReverseMap();

            CreateMap<Service_Type, Service_TypeDTO>().ReverseMap();
        }
    }
}
