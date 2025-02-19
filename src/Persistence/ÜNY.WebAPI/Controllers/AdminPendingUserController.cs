using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ÜNY.Application.Services.Abstract;
using ÜNY.Infrastructure.Persistence.Data;
using ÜNY.WebAPI.Model.AdminAproveModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPendingUserController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminPendingUserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("Pendingusers")]
        public async Task<IActionResult> Pendingusers() 
        {
        //    var pendingUsers = _context.Users
        //.Where(u => u.IsApproved == false)
        //.Select(u => new
        //{
        //    u.Id,
        //    u.Name,
        //    u.SurName,
        //    u.UserName,
        //    u.BirthPlace,
        //    u.DateofBirth
        //})
        //.ToList();

        //    return Ok(pendingUsers);

            var pendingUser = await _userService.GetPendingusersAsync();
            return Ok(pendingUser);
        }

        [HttpPost]
        [Route("Approveuser")]
        public async Task<IActionResult> ApproveUser([FromBody] AdminUserAproveViewModel model)
        {

            await _userService.ApproveUserAsync(model.UserId);
            //var user = _context.Users.SingleOrDefault(u => u.Id == model.UserId);
            //if (user == null)
            //    return NotFound(new { success = false, message = "Kullanıcı bulunamadı." });

            //user.IsApproved = true;
            //_context.Users.Update(user);
            //await   _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Kullanıcı başarıyla onaylandı." });
        }
    }
}
