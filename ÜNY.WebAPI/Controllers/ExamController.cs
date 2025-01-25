using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<Users> _userManager;

        public ExamController(DataContext dataContext, UserManager<Users> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetExamlist")]
        public async Task<IActionResult> GetExam()
        {

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var userExams = await _dataContext.Enrollment
                .Where(e => e.UserId == currentUser.Id && e.Status)
                .Include(e => e.Course)
                .ThenInclude(c => c.Exams)
                .Select(e => new
                {
                    CourseName = e.Course.CourseName,
                    Exams = e.Course.Exams.Select(exam => new
                    {
                    ExamName = exam.Name,
                    ExamDate = exam.Date
                }).ToList()
                })
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
                    return NotFound("Kullanıcının kayıtlı olduğu ders bulunamadı.");
                }

                return NotFound("Kullanıcının kayıtlı olduğu derslerde herhangi bir sınav bulunamadı.");
            }

            return Ok(userExams);
        }

        [HttpGet]
        [Route("GetExams")]
        public async Task<IActionResult> GetExams()
        {
            var exams = await _dataContext.Exams.Select(e => new
            {
                e.Id,
                e.Name,
                e.Date
            }).ToListAsync();

            return Ok(exams);
        }
    }
}
