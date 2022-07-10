using AutoMapper;
using CustomerRegistration.Core.DTOs;
using CustomerRegistration.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Service.Mappers
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<UserApp, UserAppDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CommercialActivity, CommercialActivityDto>().ReverseMap();
        }
    }
}
