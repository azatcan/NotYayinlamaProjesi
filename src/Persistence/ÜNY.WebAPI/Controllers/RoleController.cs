using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;
using ÜNY.WebAPI.Model.RoleModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Roles> _roleManager;
        private readonly UserManager<Users> _userManager;

        public RoleController(RoleManager<Roles> roleManager, UserManager<Users> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            var values = _roleManager.Roles.ToList();
            return Ok(values);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            Roles roles = new Roles
            {
                Name = model.Name,
            };
            var result = await _roleManager.CreateAsync(roles);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RoleUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return NotFound();
            }

            role.Name = model.Name;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delete = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(delete);
            return Ok();
        }

        [HttpPost]
        [Route("assignRole")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] RoleAssignmentRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı" });
            }

            var result = await _userManager.AddToRoleAsync(user, request.Role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Rol başarıyla atandı" });
            }

            return BadRequest(new { message = "Rol atanırken hata oluştu", errors = result.Errors });
        }
    }
}

