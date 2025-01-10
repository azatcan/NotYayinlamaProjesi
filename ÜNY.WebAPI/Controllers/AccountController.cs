using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.AccountModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly SignInManager<Users> _signInManager;
        public readonly IWebHostEnvironment _webHost;
        DataContext _context;

        public AccountController(UserManager<Users> userManager, RoleManager<Roles> roleManager, SignInManager<Users> signInManager, IWebHostEnvironment webHost, DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _webHost = webHost;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new JsonResult(new { Message = "Login successful." }) { StatusCode = 200 };
            }
            return new JsonResult(new { Message = "Unauthorized access." }) { StatusCode = 401 };
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users();
                if (model.ImagePath != null)
                {
                    var extension = Path.GetExtension(model.ImagePath.FileName);
                    var newimagename = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resimler/user", newimagename);
                    using (var stream = new FileStream(location, FileMode.Create))
                    {
                        await model.ImagePath.CopyToAsync(stream);
                    }
                    user.ImagePath = newimagename;
                }

                user.Name = model.Name;
                user.SurName = model.SurName;
                user.UserName = model.UserName;
                user.IdNumber = model.IdNumber;
                user.Password = model.Password;
                user.RePassword = model.RePassword;
                user.BirthPlace = model.BirthPlace;
                user.DateofBirth = model.DateofBirth;
                user.FatherName = model.FatherName;
                user.MotherName = model.MotherName;
                user.GenderId = model.GenderId;
                user.UnitİnformationId = model.UnitİnformationId;



                if (model.Password == model.RePassword)
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    await _userManager.AddToRoleAsync(user, "User");
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return NoContent();
                    }

                    return BadRequest(result.Errors);
                }
            }

            return NoContent();
        }
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        [Route("getUserRoles")]
        public async Task<IActionResult> GetUserRoles()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var rolesString = string.Join(",", userRoles);
            return Ok(rolesString);
        }

        [HttpGet]
        [Route("getGenderlist")]
        public async Task<IActionResult> GetGenderList()
        {
            var genderList = await _context.Gender
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                }).ToListAsync();

            return Ok(genderList);
        }

        [HttpGet]
        [Route("getUnitlist")]
        public async Task<IActionResult> GetUnitList()
        {
            var unitList = await _context.Unitİnformation
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.UnitName
                }).ToListAsync();

            return Ok(unitList);
        }
    }
}
