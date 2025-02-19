using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ÜNY.Application.DTOs;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _gender;

        public GenderController(IGenderService gender)
        {
            _gender = gender;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List() 
        {
            var list = await _gender.GetAllAsync();
            return Ok(list);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(GenderDTO model)
        {
            await _gender.AddAsync(model);
            await _gender.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id) 
        {
            await _gender.DeleteAsync(id);
            await _gender.SaveChangesAsync();
            return NoContent();

        }
    }
}
