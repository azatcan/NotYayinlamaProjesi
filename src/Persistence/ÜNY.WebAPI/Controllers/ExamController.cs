using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Application.DTOs;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        [Route("GetExamlist")]
        public async Task<IActionResult> GetExam()
        {
            var examUser = await _examService.GetByUserExamsAsync();

            return Ok(examUser);
            //var currentUser = await _userManager.GetUserAsync(User);
            //if (currentUser == null)
            //{
            //    return NotFound("Kullanıcı bulunamadı.");
            //}

            //var userExams = await _dataContext.Enrollment
            //    .Where(e => e.UserId == currentUser.Id && e.Status)
            //    .Include(e => e.Course)
            //    .ThenInclude(c => c.Exams)
            //    .Select(e => new
            //    {
            //        CourseName = e.Course.CourseName,
            //        Exams = e.Course.Exams.Select(exam => new
            //        {
            //        ExamName = exam.Name,
            //        ExamDate = exam.Date
            //    }).ToList()
            //    })
            //.ToListAsync();

            //if (!userExams.Any())
            //{
            //    var userCourses = await _dataContext.Enrollment
            // .Where(e => e.UserId == currentUser.Id && e.Status)
            // .Include(e => e.Course)
            // .Select(e => e.Course.CourseName)
            // .ToListAsync();

            //    if (!userCourses.Any())
            //    {
            //        return NotFound("Kullanıcının kayıtlı olduğu ders bulunamadı.");
            //    }

            //    return NotFound("Kullanıcının kayıtlı olduğu derslerde herhangi bir sınav bulunamadı.");
            //}

            //return Ok(userExams);
        }

        [HttpGet]
        [Route("GetExams")]
        public async Task<IActionResult> GetExams()
        {
            var exams = await _examService.GetAllAsync();
            return Ok(exams);
        }
    }
}
