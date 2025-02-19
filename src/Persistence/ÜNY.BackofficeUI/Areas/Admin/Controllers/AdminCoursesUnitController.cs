using Microsoft.AspNetCore.Mvc;
using ÜNY.Application.DTOs;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.WebAPI.Model.ContactModel;

namespace ÜNY.BackofficeUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class AdminCoursesUnitController : Controller
    {
        DefaultClient _client;

        public AdminCoursesUnitController(DefaultClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View(new CourseUnitInformationDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CourseUnitInformationDTO model)
        {
            var response = await _client.PostAsync<CourseUnitInformationDTO, ContactResponse>(DefaultClientEndpoint.AdminCoursesUnit.add, model);
            if (response?.Success == true)
            {
                return View(model);
            }
            return RedirectToAction("Index","Home");
        }
    }
}
