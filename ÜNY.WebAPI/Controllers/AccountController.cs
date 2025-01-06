using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(UserManager<Users> userManager, RoleManager<Roles> roleManager, SignInManager<Users> signInManager, IWebHostEnvironment webHost)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _webHost = webHost;
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
                //user.UserName = model.Name + model.SurName;
                user.UserName = model.UserName;
                user.IdNumber = model.IdNumber;
                user.Password = model.Password;
                user.RePassword = model.RePassword;
                user.BirthPlace = null;
                user.DateofBirth = null;
                user.MotherName = null;
                user.FatherName = null;
                user.GenderId = null;
                user.Gender = null;
                user.Unit = null;
                user.UnitId = null;
                user.CoursesId = null;
                user.courses = null;
                user.ContactId = null;
                user.Contactİnformation = null;


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
    }
}
