using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Abstract;
using ÜNY.WebAPI.DTOs;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeİnformationController : ControllerBase
    {
        private readonly DataContext _dataContext;
        IFeeİnformationService _feeService;
        private readonly UserManager<Users> _userManager;

        public FeeİnformationController(DataContext dataContext, IFeeİnformationService feeService, UserManager<Users> userManager)
        {
            _dataContext = dataContext;
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

            var feeId = currentUser.Feeİnformation?.Id;

            if (feeId == null)
            {
                return Ok(new { message = "Kullanıcıya ait borç bilgisi bulunamadı." });
            }

            var feeDetails = await _dataContext.Feeİnformation
             .Where(u => u.Id == feeId)
             .Select(u => new FeeİnfoDTO
             {
                 UserName = u.User.Name,
                 YourCurrentDebt = u.YourCurrentDebt,
                 YourReferenceNumber = u.YourReferenceNumber,
                 period = u.period,
                 DebtType = u.DebtType,
                 Amount = u.Amount,
                 Description = u.Description,
                 Status = u.Status,
             })
             .FirstOrDefaultAsync();

            if (feeDetails == null)
            {
                return NotFound(new { message = "Borç bilgisi bulunamadı." });
            }

            return Ok(feeDetails);
        }

    }
}
