using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.WebAPI.Model.AccountModel;
using ÜNY.WebAPI.Model.ContactModel;
using ÜNY.WebAPI.Model.CoursesModel;
using ÜNY.WebAPI.Model.EnrolmentModel;

namespace ÜNY.BackofficeUI.Controllers
{
    public class EnrolmentController : Controller
    {
        private readonly DefaultClient _client;

        public EnrolmentController(DefaultClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {

                var response = await _client.GetAsync<EnrollCoursesViewModel>(DefaultClientEndpoint.Enrolment.list);

                if (response == null)
                {
                    ViewBag.ErrorMessage = "Kurs bilgileri alınamadı.";
                    return View(new List<EnrollCoursesViewModel>());
                }

                return View(response);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Bir hata oluştu: " + ex.Message;
                return View(new List<EnrollCoursesViewModel>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollCourses(EnroleRequest model)
        {
            
            if (model.CourseIds == null || !model.CourseIds.Any())
            {
                TempData["ErrorMessage"] = "Lütfen en az bir kurs seçin.";
                return RedirectToAction("Add");
            }

            try
            {
                var response = await _client.PostAsync<EnroleRequest, dynamic>(DefaultClientEndpoint.Enrolment.add, model);

                if (response != null)
                {
                    TempData["ResultMessage"] = response.Success ?? "Kurslar başarıyla kaydedildi!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Kurs kayıt işlemi başarısız oldu.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Add");
        }


    }
}
