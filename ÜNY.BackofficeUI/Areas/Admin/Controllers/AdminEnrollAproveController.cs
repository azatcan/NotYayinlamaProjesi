using Microsoft.AspNetCore.Mvc;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.AdminEnrollAproveModel;
using ÜNY.WebAPI.Model.CoursesUnitInformationModel;

namespace ÜNY.BackofficeUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class AdminEnrollAproveController : Controller
    {
        private readonly DefaultClient _client;

        public AdminEnrollAproveController(DefaultClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> AdminApproveEnrollment()
        {
            try
            {
                var pendingEnrollments = await _client.GetAsync<List<Enrollment>>(DefaultClientEndpoint.AdminAproveEnrolments.GetPendingEnrollments);

                if (pendingEnrollments == null || !pendingEnrollments.Any())
                {
                    TempData["InfoMessage"] = "Onay bekleyen kayıt bulunmamaktadır.";
                    return View(new List<Enrollment>()); 
                }

                return View(pendingEnrollments);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                return View(new List<Enrollment>()); 
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> AdminApproveEnrollment(EnrollAproveRequest model)
        //{
        //    var response = await _client.PostAsync<EnrollAproveRequest, CourseUnitResponse>(DefaultClientEndpoint.AdminAproveEnrolments.aprove, model);
        //    if (response?.Success == true)
        //    {
        //        return View(model);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
