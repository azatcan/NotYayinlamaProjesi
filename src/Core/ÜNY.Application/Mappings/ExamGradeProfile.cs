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
    public class ExamGradeProfile : Profile
    {
        public ExamGradeProfile()
        {
            CreateMap<ExamGrade, ExamGradeDTO>().ReverseMap()
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade));
                

            CreateMap<ExamGrade, ExamGradeDTO>().ReverseMap()
                .AfterMap((src ,dest)=>{
                    dest.Exam.Name = src.ExamName;
                });

            CreateMap<ExamGrade, AdminExamGradeDTO>().ReverseMap();
        }
    }
}
