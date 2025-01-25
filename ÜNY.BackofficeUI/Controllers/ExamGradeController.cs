using Microsoft.AspNetCore.Mvc;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.WebAPI.DTOs;

namespace ÜNY.BackofficeUI.Controllers
{
    public class ExamGradeController : Controller
    {
        private readonly DefaultClient _client;

        public ExamGradeController(DefaultClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var examGradeList = await _client.GetAsync<List<ExamGradeDTO>>(DefaultClientEndpoint.ExamGrade.Get);
            return View(examGradeList);
        }
    }
}
