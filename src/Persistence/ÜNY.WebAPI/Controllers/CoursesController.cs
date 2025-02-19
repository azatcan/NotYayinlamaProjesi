using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Application.DTOs;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService _coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List() 
        {
            var courses = await _coursesService.GetAllAsync();
            return Ok(courses);
        }

      

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] CoursesDTO  model)
        {
            await _coursesService.AddAsync(model);
            await _coursesService.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id) 
        {
            await _coursesService.DeleteAsync(id);
            await _coursesService.SaveChangesAsync();
            return NoContent();
        }
    }
}
