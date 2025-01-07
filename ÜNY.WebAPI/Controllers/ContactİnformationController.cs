using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Abstract;
using ÜNY.WebAPI.Model.ContactModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactİnformationController : ControllerBase
    {
        IContactİnformationService _contact;
        DataContext _context;

        public ContactİnformationController(IContactİnformationService contact, DataContext context)
        {
            _contact = contact;
            _context = context;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult List() 
        {
            var list = _contact.List();
            return Ok(list);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(ContactViewModel model) 
        {
            Contactİnformation contact = new Contactİnformation()
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Addrees = model.Addrees,
                Phone = model.Phone,
            };
            _contact.Add(contact);
            return Ok();
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete([FromBody] ContactRequest request)
        {
            var del = _contact.GetById(request.Id);
            if (del == null)
            {
                return NotFound();
            }

            _contact.Delete(del);
            return Ok();
        }
    }
}
