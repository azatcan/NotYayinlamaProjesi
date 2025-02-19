using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Application.DTOs;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;
using URL.Domain.Repositories;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesUnitİnformationController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly IUnitİnformationService _unit;
        private readonly ICoursesService _courses;
        private readonly ICourseUnitInformationService _courseUnit;

        public CoursesUnitİnformationController(UserManager<Users> userManager, IUnitİnformationService unit, ICoursesService courses, ICourseUnitInformationService courseUnit)
        {
            _userManager = userManager;
            _unit = unit;
            _courses = courses;
            _courseUnit = courseUnit;
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

            var userUnit = await _unit.GetUserUnitInformationAsync(currentUser.Id);

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
        public async Task<IActionResult> CoursesUnitInformationAdd([FromBody] CourseUnitInformationDTO model)
        {
            if (model == null) 
                return BadRequest(ModelState);
            //var courses = await _dataContext.Courses.FindAsync(model.CourseId);
            var courses = await _courses.GetByIdAsync(model.CourseId);
            var unitInformation = await _unit.GetByIdAsync(model.UnitInformationId);

            if (courses == null || unitInformation == null)
            {
                return NotFound();
            }
            await _courseUnit.AddAsync(model);
            await _courseUnit.SaveChangesAsync();
            //var coursesUnitInformation = new CourseUnitInformation
            //{
            //    Id = Guid.NewGuid(),
            //    CourseId = model.CourseId,
            //    UnitInformationId = model.UnitInformationId,
            //};
            //_dataContext.CoursesUnitInformation.Add(coursesUnitInformation);
            //await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
