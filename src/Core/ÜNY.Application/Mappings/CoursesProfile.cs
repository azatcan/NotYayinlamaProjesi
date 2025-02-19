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
    public class CoursesProfile : Profile
    {
        public CoursesProfile() 
        {
            CreateMap<Courses, CoursesDTO>().ReverseMap();

        }
    }
}
