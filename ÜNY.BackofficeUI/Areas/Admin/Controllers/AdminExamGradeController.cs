using Microsoft.AspNetCore.Mvc;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.WebAPI.DTOs;
using ÜNY.WebAPI.Model.AdminExamGrade;
using ÜNY.WebAPI.Model.ContactModel;
using ÜNY.WebAPI.Model.ExamModel;
using ÜNY.WebAPI.Model.UsersModel;

namespace ÜNY.BackofficeUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class AdminExamGradeController : Controller
    {
        private readonly DefaultClient _client;

        public AdminExamGradeController(DefaultClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var exams = await _client.GetAsync<List<ExamViewModel>>(DefaultClientEndpoint.Exam.GetExams);
            return View(exams);
        }

        [HttpGet("ManageGrades/{examId}")]
        public async Task<IActionResult> ManageGrades(Guid examId)
        {
            var endpoint = $"{DefaultClientEndpoint.Users.BaseGetStudentsForCourse}/{examId}";
            var students = await _client.GetAsync<List<StudentViewModel>>(endpoint);

            ViewBag.ExamId = examId; 
            return View(students);
        }

        [HttpPost("AddGrade")]
        public async Task<IActionResult> AddGrade(AdminExamGradeViewModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("Geçersiz veri!");
            }

            var response = await _client.PostAsync<AdminExamGradeViewModel, ContactResponse>(DefaultClientEndpoint.AdminExamGrade.Add, model);

            if (response.Success)
            {
                TempData["Message"] = "Not başarıyla eklendi.";
                return RedirectToAction("Index","Home");
            }

            TempData["Error"] = "Not eklenirken bir hata oluştu.";
            return RedirectToAction("ManageGrades", new { examId = model.ExamId });
        }
    }
}

