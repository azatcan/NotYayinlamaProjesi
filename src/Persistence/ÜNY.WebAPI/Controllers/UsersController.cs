using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ÜNY.Application.Services.Abstract;
using ÜNY.Domain.Entities;
using ÜNY.Infrastructure.Persistence.Data;
using ÜNY.WebAPI.Model.UsersModel;

namespace ÜNY.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<Users> _userManager;
        private readonly IUserService _userService;
        private readonly IEnrollmentService _enrollmentService;

        public UsersController(UserManager<Users> userManager, IUserService userService, IEnrollmentService enrollmentService)
        {
            _userManager = userManager;
            _userService = userService;
            _enrollmentService = enrollmentService;
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
            var userWithDetails = await _userService.GetUserByIdAsync(currentUser.Id);

            if (userWithDetails == null)
            {
                return NotFound();
            }

            return Ok(userWithDetails);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users); 
        }


        [HttpGet("GetEnrolledStudents/{examId}")]
        public async Task<IActionResult> GetEnrolledStudents(Guid examId)
        {
            var students = await _enrollmentService.GetEnrolledStudentsAsync(examId);

            return Ok(students);
        }
    }
}
