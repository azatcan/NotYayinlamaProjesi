using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<Users> _userManager;

        public ContactİnformationController(IContactİnformationService contact, DataContext context, UserManager<Users> userManager)
        {
            _contact = contact;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            var list = _contact.List();
            return Ok(list);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] ContactViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }
            Contactİnformation contact = new Contactİnformation()
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Addrees = model.Addrees,
                Phone = model.Phone,
                UserId = currentUser.Id

            };
            _contact.Add(contact);

            currentUser.ContactİnformationId = contact.Id;
            var updateResult = await _userManager.UpdateAsync(currentUser);

            if (!updateResult.Succeeded)
            {
                return BadRequest(updateResult.Errors);
            }
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
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

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] ContactViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            var contact = await _context.Contactİnformation.FirstOrDefaultAsync(c => c.Id == currentUser.ContactİnformationId);
            if (contact == null)
            {
                return NotFound();
            }

            contact.Email = model.Email;
            contact.Addrees = model.Addrees;
            contact.Phone = model.Phone;

            _contact.Update(contact);

            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
