using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ÜNY.Application.DTOs;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public AdminExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] ExamDTO model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("Geçersiz veri.");
            }

            //var exam = new Exam
            //{
            //    Id = Guid.NewGuid(),
            //    Name = model.ExamName,
            //    Date = model.ExamDate,
            //    CourseId = model.CourseId
            //};

            //await _dataContext.Exams.AddAsync(exam);
            //await _dataContext.SaveChangesAsync();
            await _examService.AddAsync(model);
            await _examService.SaveChangesAsync();

            return Ok(new { Message = "Sınav başarıyla eklendi." });
        }
    }
}
