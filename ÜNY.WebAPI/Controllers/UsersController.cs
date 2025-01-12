using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.UsersModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        DataContext _context;
        private readonly UserManager<Users> _userManager;

        public UsersController(DataContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) 
            {
                return NotFound();
            }
            //await _context.Users
            //.Include(u => u.Unitİnformation)  
            //.FirstOrDefaultAsync(u => u.Id == currentUser.Id);
            return Ok(currentUser);
        }
    }
}
