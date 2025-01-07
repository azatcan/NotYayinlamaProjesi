using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Abstract;
using ÜNY.WebAPI.Model.UnitModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitİnformationController : ControllerBase
    {
        IUnitİnformationService _unitService;
        DataContext _context;

        public UnitİnformationController(IUnitİnformationService unitService, DataContext context)
        {
            _unitService = unitService;
            _context = context;
        }

        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            var unit = _unitService.List(); 
            return Ok(unit);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(UnitViewModel model) 
        {
            Unitİnformation unit = new Unitİnformation()
            {
                Id = Guid.NewGuid(),
                FacultyName = model.FacultyName,
                UnitName = model.UnitName,
            };
            _context.Add(unit);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete([FromBody] UnitRequest request) 
        {
            var unit = _unitService.GetById(request.id);
            if (unit == null) 
            {
                return NotFound();
            }
            _unitService.Delete(unit);
            return Ok();

        }
        [HttpPost]
        [Route("Update")]
        public IActionResult Update(Unitİnformation unit,Guid Id)
        {
            var existingEntity = _unitService.GetById(Id);
            if (existingEntity == null)
            {
                return NotFound();
            }
            else
            {
                existingEntity.FacultyName = unit.FacultyName;
                existingEntity.UnitName = unit.UnitName;
                _unitService.Update(existingEntity);
                return Ok();
            }

        }
    }
}
