using Microsoft.AspNetCore.Mvc;
using ÜNY.Application.DTOs;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.ContactModel;

namespace ÜNY.BackofficeUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class AdminExamController : Controller
    {
        private readonly DefaultClient _client;

        public AdminExamController(DefaultClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var courses = await _client.GetAsync<List<CoursesDTO>>(DefaultClientEndpoint.Courses.GetAllCourses);
            ViewBag.Courses = courses ?? new List<CoursesDTO>();

            return View(new ExamDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ExamDTO model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Geçersiz form verileri.";
                return View(model);
            }

            var response = await _client.PostAsync<ExamDTO, ContactResponse>(DefaultClientEndpoint.AdminExam.add, model);

            if (response != null)
            {
                TempData["SuccessMessage"] = "Sınav başarıyla eklendi.";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Sınav eklenirken hata oluştu.";
            return View(model);
        }
    }
}
