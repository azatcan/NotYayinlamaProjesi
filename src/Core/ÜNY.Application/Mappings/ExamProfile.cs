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
    public class ExamProfile : Profile
    {
        public ExamProfile() 
        {
            CreateMap<Exam, ExamDTO>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId)) 
                .ForMember(dest => dest.ExamId, opt => opt.MapFrom(src => src.Id)) 
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Name)) 
                .ForMember(dest => dest.ExamDate, opt => opt.MapFrom(src => src.Date));

            CreateMap<Exam, CourseExamDTO>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName))
            .ForMember(dest => dest.Exams, opt => opt.MapFrom(src => src.Course.Exams.Select(exam => new ExamDTO
            {
                ExamId = exam.Id,
                ExamName = exam.Name
            }).ToList()));

        }
    }
}
