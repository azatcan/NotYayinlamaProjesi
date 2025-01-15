using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.CoursesUnitInformationModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesUnitİnformationController : ControllerBase
    {
        DataContext _dataContext;

        public CoursesUnitİnformationController(DataContext dataContext)
        {
            _dataContext = dataContext;
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
