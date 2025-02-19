using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Domain.Entities;

namespace ÜNY.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Users, UserDTO>()
                .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender != null ? src.Gender.Name : null))
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unitİnformation != null ? src.Unitİnformation.UnitName : null))
                .ReverseMap();
        }
    }
}
