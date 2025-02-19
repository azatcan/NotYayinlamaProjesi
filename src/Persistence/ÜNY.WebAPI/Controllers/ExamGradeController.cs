using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Application.DTOs;
using ÜNY.Application.Exceptions;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamGradeController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly IExamGradeService _examGradeService;

        public ExamGradeController(UserManager<Users> userManager, IExamGradeService examGradeService)
        {
            _userManager = userManager;
            _examGradeService = examGradeService;
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

            try
            {
                var examGrades = await _examGradeService.GetUserCoursesWithGradesAsync(currentUser.Id);

                if (examGrades == null)
                {
                    return NotFound(new { message = "Sınav bilgisi bulunamadı." });
                }

                return Ok(new List<ExamGradeDTO> { examGrades });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
            }
        }
    }
}
