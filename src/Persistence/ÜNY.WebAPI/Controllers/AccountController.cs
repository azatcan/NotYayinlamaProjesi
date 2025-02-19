using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;
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
        private readonly IUnitİnformationService _unit;
        private readonly IGenderService _genderService;
        private readonly DataContext _context;

        public AccountController(UserManager<Users> userManager, RoleManager<Roles> roleManager, SignInManager<Users> signInManager, IWebHostEnvironment webHost, IUnitİnformationService unit, IGenderService genderService, DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _webHost = webHost;
            _unit = unit;
            _genderService = genderService;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (result.Succeeded)
            {
                if (!user.IsApproved) {
                    return new JsonResult(new { Message = "Hesabınız henüz onaylanmamış. Lütfen admin onayını bekleyin." }) { StatusCode = 401 };
                }

                return new JsonResult(new { Message = "Login successful." }) { StatusCode = 200 };
            }
            return Unauthorized();
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
                user.ContactİnformationId = null;
                user.IsApproved = false;



                if (model.Password == model.RePassword)
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    await _userManager.AddToRoleAsync(user, "User");
                    if (result.Succeeded)
                    {
                        return Ok(new { message = "Kayıt işlemi başarılı. Admin onayı bekleniyor." });
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        //return NoContent();
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
            var genderList = await _genderService.GetAllAsync();

            return Ok(genderList);
        }

        [HttpGet]
        [Route("getUnitlist")]
        public async Task<IActionResult> GetUnitList()
        {
            var unitList = await _unit.GetAllAsync();

            return Ok(unitList);
        }
    }
}
