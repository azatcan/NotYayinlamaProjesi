using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.AdminExamGrade;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminExamGradeController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<Users> _userManager;

        public AdminExamGradeController(DataContext dataContext, UserManager<Users> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddGarde([FromBody] AdminExamGradeViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var examGrade = new ExamGrade
            {
                Id = Guid.NewGuid(),
                ExamId = model.ExamId,
                UserId = model.UserId,
                Grade = model.Grade
            };

            await _dataContext.ExamGrades.AddAsync(examGrade);
            await _dataContext.SaveChangesAsync();

            return Ok(new { Message = "Grade added successfully" });
        }
    }
}
