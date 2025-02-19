using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Domain.Entities;
using ÜNY.Domain.Repositories;
using ÜNY.Infrastructure.Persistence.Data;
using URL.Infrastructure.Repositories;

namespace ÜNY.Infrastructure.Repositories
{
    public class ExamGradeRepository : GenericRepository<ExamGrade, DataContext>, IExamGradeRepository
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<Users> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ExamGradeRepository(DataContext dataContext, UserManager<Users> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(dataContext) 
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ExamGrade> GetUserCoursesWithGradesAsync(Guid userId)
        {

            var userCoursesWithGrades = await _dataContext.Courses
               .Where(c => c.Enrollments.Any(e => e.UserId == userId))
               .Select(c => new ExamGradeDTO
               {
                   //CourseName = c.CourseName,
                   ExamName = c.Exams.FirstOrDefault() != null ? c.Exams.FirstOrDefault().Name : "Sınav Girilmedi",
                   Grade = c.Exams.FirstOrDefault() != null &&
                       c.Exams.FirstOrDefault().ExamGrades.Any(g => g.UserId == userId)
                       ? (decimal?)c.Exams.FirstOrDefault().ExamGrades.FirstOrDefault(g => g.UserId == userId).Grade
                       : null
               })
                   .ToListAsync();

            var examGrades = _mapper.Map<List<ExamGrade>>(userCoursesWithGrades);

            return examGrades.FirstOrDefault();
        }
    }
}
