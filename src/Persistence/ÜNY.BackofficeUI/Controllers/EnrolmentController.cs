using Azure;
using Microsoft.AspNetCore.Mvc;
using ÜNY.Application.DTOs;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.WebAPI.Model.AccountModel;
using ÜNY.WebAPI.Model.ContactModel;

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

                var response = await _client.GetAsync<EnrollCoursesDTO>(DefaultClientEndpoint.Enrolment.list);

                if (response == null)
                {
                    ViewBag.ErrorMessage = "Kurs bilgileri alınamadı.";
                    return View(new List<EnrollCoursesDTO>());
                }

                return View(response);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Bir hata oluştu: " + ex.Message;
                return View(new List<EnrollCoursesDTO>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollCourses(EnrollmentDTO model)
        {
            
            if (model.CourseIds == null || !model.CourseIds.Any())
            {
                TempData["ErrorMessage"] = "Lütfen en az bir kurs seçin.";
                return RedirectToAction("Add");
            }

            try
            {
                var response = await _client.PostAsync<EnrollmentDTO, dynamic>(DefaultClientEndpoint.Enrolment.add, model);

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
