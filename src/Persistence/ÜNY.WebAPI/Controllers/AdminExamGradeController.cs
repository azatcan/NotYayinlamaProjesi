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
    public class AdminExamGradeController : ControllerBase
    {
        private readonly IExamGradeService _examGradeService;

        public AdminExamGradeController(IExamGradeService examGradeService)
        {
            _examGradeService = examGradeService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddGarde([FromBody] AdminExamGradeDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _examGradeService.AddGrade(model);
            await _examGradeService.SaveChangesAsync();

            //var examGrade = new ExamGrade
            //{
            //    Id = Guid.NewGuid(),
            //    ExamId = model.ExamId,
            //    UserId = model.UserId,
            //    Grade = model.Grade
            //};

            //await _dataContext.ExamGrades.AddAsync(examGrade);
            //await _dataContext.SaveChangesAsync();

            return Ok(new { Message = "Grade added successfully" });
        }
    }
}
