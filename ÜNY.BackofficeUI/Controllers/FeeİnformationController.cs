using Microsoft.AspNetCore.Mvc;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.WebAPI.DTOs;

namespace ÜNY.BackofficeUI.Controllers
{
    public class FeeİnformationController : Controller
    {
        private readonly DefaultClient _client;

        public FeeİnformationController(DefaultClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> list()
        {
            var feeList = await _client.GetAsync<FeeİnfoDTO>(DefaultClientEndpoint.Feeİnfo.FeeİnfoList);

            if (feeList.UserName == null)
            {
                ViewBag.Message = "Borç bilginiz bulunmamaktadır."; 
                return View("List", null);
            }
            return View(feeList);
        }
    }
}
