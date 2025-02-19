using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using ÜNY.Application.DTOs;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitİnformationController : ControllerBase
    {
        private readonly IUnitİnformationService _unitService;

        public UnitİnformationController(IUnitİnformationService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var unit = await _unitService.GetAllAsync(); 
            return Ok(unit);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(UnitİnformationDTO model) 
        {
            await _unitService.AddAsync(model);
            await _unitService.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id) 
        {
            await _unitService.DeleteAsync(id);
            await _unitService.SaveChangesAsync();
            return Ok();

        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(UnitİnformationDTO unit)
        {
            await _unitService.UpdateAsync(unit);
            await _unitService.SaveChangesAsync();
            return NoContent();
        }
    }
}
