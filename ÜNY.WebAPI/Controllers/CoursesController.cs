using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Abstract;
using ÜNY.WebAPI.Model.CoursesModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        ICoursesService _coursesService;
        DataContext _dataContext;

        public CoursesController(ICoursesService coursesService, DataContext dataContext)
        {
            _coursesService = coursesService;
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List() 
        {
            var courses = await _dataContext.Courses.ToListAsync();
            return Ok(courses);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] CoursesViewModel model)
        {
            Courses courses = new Courses()
            {
                CourseName = model.CourseName,                
            };
            _coursesService.Add(courses);
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(CoursesRequest request) 
        {
            var getId = _coursesService.GetById(request.Id);
            if (getId == null) 
            {
                return NotFound();
            }
            else
            {
                _coursesService.Delete(getId);
                return Ok();
            }
        }
    }
}
