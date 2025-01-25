using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.DTOs;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamGradeController : ControllerBase
    {
        private readonly DataContext _datacontext;
        private readonly UserManager<Users> _userManager;

        public ExamGradeController(DataContext datacontext, UserManager<Users> userManager)
        {
            _datacontext = datacontext;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound("Kullanıcı Bulunmadı.");
            }

            var userCoursesWithGrades = await _datacontext.Courses
                .Where(c => c.Enrollments.Any(e => e.UserId == currentUser.Id))
                .Select(c => new ExamGradeDTO
                {
                    CourseName = c.CourseName,
                    ExamName = c.Exams.FirstOrDefault() != null ? c.Exams.FirstOrDefault().Name : "Sınav Girilmedi",
                    Grade = c.Exams.FirstOrDefault() != null &&
                        c.Exams.FirstOrDefault().ExamGrades.Any(g => g.UserId == currentUser.Id)
                        ? (decimal?)c.Exams.FirstOrDefault().ExamGrades.FirstOrDefault(g => g.UserId == currentUser.Id).Grade
                        : null
                })
                    .ToListAsync();


            if (!userCoursesWithGrades.Any())
            {
                return NotFound("Kullanıcı için ders bulunamadı.");
            }

            return Ok(userCoursesWithGrades);
        }
    }
}
