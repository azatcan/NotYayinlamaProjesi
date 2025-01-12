using Microsoft.AspNetCore.Mvc;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.Domain.Entities;

namespace ÜNY.BackofficeUI.Controllers
{
    public class UsersController : Controller
    {
        DefaultClient client;

        public UsersController(DefaultClient client)
        {
            this.client = client;
        }

        public async Task<IActionResult> List()
        {
            var user = await client.GetAsync<Users>(DefaultClientEndpoint.Users.List);
            return View(user);
        }
    }
}
