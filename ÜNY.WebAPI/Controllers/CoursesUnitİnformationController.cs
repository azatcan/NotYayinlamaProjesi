using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.CoursesUnitInformationModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesUnitİnformationController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        DataContext _dataContext;

        public CoursesUnitİnformationController(UserManager<Users> userManager, DataContext dataContext)
        {
            _userManager = userManager;
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("GetUserUnitCourses")]
        public async Task<IActionResult> GetUserUnitCourses()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var userUnit = await _dataContext.Unitİnformation
                .Include(u => u.CourseUnitInformations)
                .ThenInclude(cui => cui.Course)
                .FirstOrDefaultAsync(u => u.Users.Any(u => u.Id == currentUser.Id));

            if (userUnit == null)
            {
                return NotFound("User's unit information not found.");
            }

            var response = new
            {
                UnitName = userUnit.UnitName,
                AvailableCourses = userUnit.CourseUnitInformations.Select(cui => new
                {
                    Id = cui.Course.Id,
                    CourseName = cui.Course.CourseName
                })
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CoursesUnitInformationAdd([FromBody] CoursesUnitInformationviewModel model)
        {
            if (model == null) 
                return BadRequest(ModelState);
            var courses = await _dataContext.Courses.FindAsync(model.CourseId);
            var unitInformation = await _dataContext.Unitİnformation.FindAsync(model.UnitInformationId);

            if (courses == null || unitInformation == null)
            {
                return NotFound();
            }
            var coursesUnitInformation = new CourseUnitInformation
            {
                Id = Guid.NewGuid(),
                CourseId = model.CourseId,
                UnitInformationId = model.UnitInformationId,
            };
            _dataContext.CoursesUnitInformation.Add(coursesUnitInformation);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
