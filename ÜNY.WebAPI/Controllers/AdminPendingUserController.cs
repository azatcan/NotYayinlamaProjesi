using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using ÜNY.Domain.Data;
using ÜNY.WebAPI.Model.AdminAproveModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPendingUserController : ControllerBase
    {
        DataContext _context;

        public AdminPendingUserController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Pendingusers")]
        public IActionResult Pendingusers() 
        {
            var pendingUsers = _context.Users
        .Where(u => u.IsApproved == false)
        .Select(u => new
        {
            u.Id,
            u.Name,
            u.SurName,
            u.UserName,
            u.BirthPlace,
            u.DateofBirth
        })
        .ToList();

            return Ok(pendingUsers);
        }

        [HttpPost]
        [Route("Approveuser")]
        public async Task<IActionResult> ApproveUser([FromBody] AdminUserAproveViewModel model)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == model.UserId);
            if (user == null)
                return NotFound(new { success = false, message = "Kullanıcı bulunamadı." });

            user.IsApproved = true;
            _context.Users.Update(user);
            await   _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Kullanıcı başarıyla onaylandı." });
        }
    }
}
