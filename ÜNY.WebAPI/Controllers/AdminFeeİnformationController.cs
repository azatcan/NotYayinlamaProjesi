using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.FeeİnfoModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminFeeİnformationController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<Users> _userManager;

        public AdminFeeİnformationController(DataContext dataContext, UserManager<Users> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("CreateFee")]
        public async Task<IActionResult> CreateFee([FromBody] FeeİnfoViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data.");
            }

            var feeInformation = new Feeİnformation
            {
                Id = Guid.NewGuid(),
                YourReferenceNumber = model.YourReferenceNumber,
                YourCurrentDebt = model.YourCurrentDebt,
                period = model.period,
                DebtType = model.DebtType,
                Amount = model.Amount,
                Description = model.Description,
                Status = false, 
                UserId = model.UserId
            };

            _dataContext.Feeİnformation.Add(feeInformation);
            await _dataContext.SaveChangesAsync();

            var user = await _dataContext.Users
                             .Include(u => u.Feeİnformation)
                             .FirstOrDefaultAsync(u => u.Id == model.UserId);
            if (user != null)
            {
                user.FeeİnformationId = feeInformation.Id; 
                _dataContext.Users.Update(user);
                await _dataContext.SaveChangesAsync();
            }

            return Ok(new { Message = "Fee information added successfully and User updated." });
        }
    }
}
