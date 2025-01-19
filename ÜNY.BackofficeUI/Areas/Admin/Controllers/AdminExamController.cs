using Microsoft.AspNetCore.Mvc;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.AdminExamModel;
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
            var courses = await _client.GetAsync<List<Courses>>(DefaultClientEndpoint.Courses.GetAllCourses);
            ViewBag.Courses = courses ?? new List<Courses>();

            return View(new AdminExemViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdminExemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Geçersiz form verileri.";
                return View(model);
            }

            var response = await _client.PostAsync<AdminExemViewModel, ContactResponse>(DefaultClientEndpoint.AdminExam.add, model);

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
