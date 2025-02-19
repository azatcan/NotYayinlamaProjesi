using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Application.DTOs;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolmentController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<Users> _userManager;

        public EnrolmentController(DataContext dataContext, UserManager<Users> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }
        [HttpPost]
        [Route("EnrollCourses")]
        public async Task<IActionResult> enrollCourses([FromBody] EnrollmentDTO model)
        {

            if (model.CourseIds == null || !model.CourseIds.Any())
            {
                return BadRequest("No courses selected.");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound("User not found.");
            }

            var userUnit = await _dataContext.Unitİnformation
                .Include(u => u.CourseUnitInformations)
                .ThenInclude(cui => cui.Course)
                .FirstOrDefaultAsync(u => u.Users.Any(u => u.Id == currentUser.Id));

            if (userUnit == null)
            {
                return BadRequest("User's unit information not found.");
            }

            var allowedCourseIds = userUnit.CourseUnitInformations
                .Select(cui => cui.CourseId)
                .ToList();

            var invalidCourses = model.CourseIds.Except(allowedCourseIds).ToList();
            if (invalidCourses.Any())
            {
                return BadRequest(new
                {
                    Message = "Some selected courses are not available for the user's unit.",
                    InvalidCourses = invalidCourses
                });
            }

            var enrollments = model.CourseIds.Select(courseId => new Enrollment
            {
                Id = Guid.NewGuid(),
                UserId = currentUser.Id,
                CourseId = courseId,
                Status = false,
            }).ToList();

            await _dataContext.Enrollment.AddRangeAsync(enrollments);
            await _dataContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Courses enrolled successfully. Waiting for admin approval.",
                EnrolledCourses = enrollments.Select(e => new { e.CourseId })
            });
        }
    }
}
