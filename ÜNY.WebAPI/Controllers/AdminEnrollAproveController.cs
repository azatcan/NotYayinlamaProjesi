using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Domain.Data;
using ÜNY.WebAPI.Model.AdminEnrollAproveModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminEnrollAproveController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public AdminEnrollAproveController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("GetPendingEnrollments")]
        public async Task<IActionResult> GetPendingEnrollments()
        {
            var pendingEnrollments = await _dataContext.Enrollment
                .Where(e => !e.Status) 
                .ToListAsync();

            if (!pendingEnrollments.Any())
            {
                return NotFound(new { Message = "No pending enrollments found." });
            }

            return Ok(pendingEnrollments);
        }


        [HttpPost]
        [Route("ApproveEnrollment")]
        public async Task<IActionResult> ApproveEnrollment([FromBody] EnrollAproveRequest model)
        {
            var enrollment = await _dataContext.Enrollment
                .FirstOrDefaultAsync(e => e.Id == model.Id);

            if (enrollment == null)
            {
                return NotFound("Enrollment not found.");
            }

            enrollment.Status = true; 
            _dataContext.Enrollment.Update(enrollment);
            await _dataContext.SaveChangesAsync();

            return Ok(new { Message = "Enrollment approved successfully." });
        }
    }
}
