using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ÜNY.Application.DTOs;
using ÜNY.Application.Exceptions;
using ÜNY.Domain.Entities;
using ÜNY.Domain.Repositories;
using ÜNY.Infrastructure.Persistence.Data;
using URL.Infrastructure.Repositories;

namespace ÜNY.Infrastructure.Repositories
{
    public class ExamRepository : GenericRepository<Exam, DataContext>, IExamRepository
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<Users> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExamRepository(DataContext dataContext, UserManager<Users> userManager, IHttpContextAccessor httpContextAccessor) : base(dataContext)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Exam>> GetuserExamsAsync()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var currentUser = await _userManager.GetUserAsync(user);
            if (currentUser == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }

            var userExams = await _dataContext.Enrollment
               .Where(e => e.UserId == currentUser.Id && e.Status)
               .Include(e => e.Course)
               .ThenInclude(c => c.Exams)
               .SelectMany(e => e.Course.Exams)  
               .Where(exam => exam != null)  
               .ToListAsync();

            if (!userExams.Any())
            {
                var userCourses = await _dataContext.Enrollment
                 .Where(e => e.UserId == currentUser.Id && e.Status)
                 .Include(e => e.Course)
                 .Select(e => e.Course.CourseName)
                 .ToListAsync();

                if (!userCourses.Any())
                {
                    throw new NotFoundException("Kullanıcının kayıtlı olduğu ders bulunamadı.");
                }

                throw new NotFoundException("Kullanıcının kayıtlı olduğu derslerde herhangi bir sınav bulunamadı.");
            }

            return userExams;
        }
    }
}
