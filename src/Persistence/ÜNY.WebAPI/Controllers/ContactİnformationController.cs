using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Application.DTOs;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;
using ÜNY.WebAPI.Model.ContactModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactİnformationController : ControllerBase
    {
        private readonly IContactİnformationService _contact;
        private readonly UserManager<Users> _userManager;

        public ContactİnformationController(IContactİnformationService contact, UserManager<Users> userManager)
        {
            _contact = contact;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            var list = await _contact.GetAllAsync();
            return Ok(list);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] ContactİnformationDTO model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            var result = await _contact.AddContactInformationAsync(model, currentUser.Id);

            currentUser.ContactİnformationId = result.Id;
            var updateResult = await _userManager.UpdateAsync(currentUser);

            if (!updateResult.Succeeded)
            {
                return BadRequest(updateResult.Errors);
            }
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid Id)
        {
            await _contact.DeleteAsync(Id);
            await _contact.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] ContactİnformationDTO model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            var updateResult = await _contact.UpdateContactInformationAsync(model, currentUser);

            if (updateResult == null)
            {
                return NotFound("İletişim bilgisi güncellenemedi.");
            }

            return Ok("İletişim bilgisi başarıyla güncellendi.");

        }
    }
}
