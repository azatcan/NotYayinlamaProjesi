using Microsoft.AspNetCore.Mvc;

namespace ÜNY.BackofficeUI.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
