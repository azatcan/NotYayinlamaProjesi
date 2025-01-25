using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ÜNY.Domain.Data;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.DTOs;
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
            var userWithDetails = await _context.Users
            .Where(u => u.Id == currentUser.Id)
            .Select(u => new UserDTO
            {
                BirthPlace = u.BirthPlace,
                ImagePath = u.ImagePath,
                DateofBirth = u.DateofBirth,
                FatherName = u.FatherName,
                Gender = u.Gender.Name,
                IdNumber = u.IdNumber,
                MotherName = u.MotherName,
                Id = u.Id,
                Name = u.Name,
                SurName = u.SurName,
                UnitName = u.Unitİnformation.UnitName,
                FacultyName = u.Unitİnformation.FacultyName,
                Email = u.Contactİnformation.Email,
                Phone = u.Contactİnformation.Phone,
                Addrees = u.Contactİnformation.Addrees,
            })
                .FirstOrDefaultAsync();

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
            var users = await _context.Users.ToListAsync();
            return Ok(users); 
        }


        [HttpGet("GetEnrolledStudents/{examId}")]
        public async Task<IActionResult> GetEnrolledStudents(Guid examId)
        {
            var students = await _context.Enrollment
                .Where(en => en.Course.Exams.Any(ex => ex.Id == examId) && en.Status)
                .Select(en => new
                {
                    en.UserId,
                    en.Users.Name 
                }).ToListAsync();

            return Ok(students);
        }
    }
}
