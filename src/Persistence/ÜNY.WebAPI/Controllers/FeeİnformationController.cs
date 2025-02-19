using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Application.Exceptions;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeİnformationController : ControllerBase
    {
        private readonly IFeeİnformationService _feeService;
        private readonly UserManager<Users> _userManager;

        public FeeİnformationController(IFeeİnformationService feeService, UserManager<Users> userManager)
        {
            _feeService = feeService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı." });
            }

            try
            {
                var feeDetails = await _feeService.GetUserFeeAsync(currentUser.Id);
                return Ok(feeDetails);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
