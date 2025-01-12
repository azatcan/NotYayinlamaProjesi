using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.AccountModel;

namespace ÜNY.BackofficeUI.Controllers
{
    public class AccountController : Controller
    {
        DefaultClient client;

        public AccountController(DefaultClient client)
        {
            this.client = client;
        }

        [HttpGet]
        public IActionResult login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await client.PostAsync<LoginModel, LoginResponse>(DefaultClientEndpoint.Authentice.Login, model);
                if (response.Success)
                {
                    var userRoles = await client.GetAsync<string>(DefaultClientEndpoint.Authentice.GetUserRoles);

                    HttpContext.Session.SetString("UserRoles", userRoles);

                    HttpContext.Session.SetString("UserName", model.UserName);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("login", "Account");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Register()        
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await client.PostAsync(DefaultClientEndpoint.Authentice.Register, model);
                if (response != null && response.Success == true)
                {
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "Kayıt başarılı. Admin onayını bekleyiniz.";
                    //HttpContext.Session.SetString("UserName", model.UserName);
                    return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }

        public async Task<ActionResult> LogOut()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5282");

                var response = await client.PostAsync("/api/Account/logout", null);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.Clear();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Error");
                }
            }
        }
    }
}
