using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.AdminAproveModel;

namespace ÜNY.BackofficeUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class AdminAproveController : Controller
    {
        DefaultClient _client;

        public AdminAproveController(DefaultClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> list()
        {
            var userPendingList = await _client.GetAsync<List<Users>>(DefaultClientEndpoint.AdminAprove.list);
            return View(userPendingList);
        }

        //[HttpPost]
        //public async Task<IActionResult> ApproveUser(Guid userId)
        //{

        //    var response = await _client.PostAsync<Guid, AproveResponse>(DefaultClientEndpoint.AdminAprove.aprove, userId);

        //    if (response.Success)
        //    {
        //        TempData["SuccessMessage"] = "Kullanıcı başarıyla onaylandı.";
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "Kullanıcı onaylama işlemi başarısız oldu.";
        //    }

        //    return RedirectToAction("List");
        //}
    }
}
