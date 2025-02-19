using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class AdminFeeİnformationController : ControllerBase
    {

        private readonly IFeeİnformationService _fee;

        public AdminFeeİnformationController(IFeeİnformationService fee)
        {
            _fee = fee;
        }

        [HttpPost]
        [Route("CreateFee")]
        public async Task<IActionResult> CreateFee([FromBody] FeeİnfoDTO model)
        {
            try
            {
                await _fee.AddAsync(model);
                await _fee.SaveChangesAsync();
                return Ok(new { Message = "Fee information added successfully and User updated." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}

