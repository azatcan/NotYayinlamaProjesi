using Microsoft.AspNetCore.Mvc;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.WebAPI.DTOs;

namespace ÜNY.BackofficeUI.Controllers
{
    public class ExamController : Controller
    {

        private readonly DefaultClient _client;

        public ExamController(DefaultClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _client.GetAsync<IEnumerable<CourseExamDTO>>(DefaultClientEndpoint.Exam.Get);
            return View(list);
        }
    }
}
