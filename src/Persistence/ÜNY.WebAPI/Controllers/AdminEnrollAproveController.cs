using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Application.DTOs;
using ÜNY.Application.Services.Abstract;
using ÜNY.Infrastructure.Persistence.Data;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminEnrollAproveController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public AdminEnrollAproveController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        [Route("GetPendingEnrollments")]
        public async Task<IActionResult> GetPendingEnrollments()
        {
            var pendingEnrollments = await _enrollmentService.GetPendingEnrollmentsAsync();

            if (!pendingEnrollments.Any())
            {
                return NotFound(new { Message = "No pending enrollments found." });
            }

            return Ok(pendingEnrollments);
        }


        [HttpPost]
        [Route("ApproveEnrollment")]
        public async Task<IActionResult> ApproveEnrollment([FromBody] EnrollAproveDTO model)
        {
            try
            {
                await _enrollmentService.ApproveEnrollmentAsync(model);
                return Ok(new { Message = "Enrollments approved successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }
    }
}
