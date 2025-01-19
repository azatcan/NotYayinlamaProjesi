using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.AdminExamModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminExamController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<Users> userManager;

        public AdminExamController(DataContext dataContext, UserManager<Users> userManager)
        {
            _dataContext = dataContext;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] AdminExemViewModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("Geçersiz veri.");
            }

            var exam = new Exam
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Date = model.Date,
                CourseId = model.CourseId
            };

            await _dataContext.Exams.AddAsync(exam);
            await _dataContext.SaveChangesAsync();

            return Ok(new { Message = "Sınav başarıyla eklendi." });
        }
    }
}
