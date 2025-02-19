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
    public class ContactİnformationProfile : Profile
    {
        public ContactİnformationProfile()
        {
            CreateMap<Contactİnformation,ContactİnformationDTO>().ReverseMap();
        }
    }
}
