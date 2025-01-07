using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Abstract;
using ÜNY.WebAPI.Model.GenderModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        IGenderService _gender;
        DataContext _dataContext;

        public GenderController(IGenderService gender, DataContext dataContext)
        {
            _gender = gender;
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult List() 
        {
            var list = _gender.List();
            return Ok(list);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(GenderViewModel model)
        {
            Gender gender = new Gender()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
            };
            _gender.Add(gender);
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(GenderRequest request) 
        {
            var gender = _gender.GetById(request.Id);
            if (gender == null) 
            {
                return NotFound();
            }
            _gender.Delete(gender);
            return Ok();

        }
    }
}
