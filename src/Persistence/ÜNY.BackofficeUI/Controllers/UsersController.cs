using Microsoft.AspNetCore.Mvc;
using ÜNY.Application.DTOs;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.AccountModel;
using ÜNY.WebAPI.Model.ContactModel;

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
            var user = await client.GetAsync<UserDTO>(DefaultClientEndpoint.Users.List);
            return View(user);
        }

        public async Task<IActionResult> UnitList()
        {
            var user = await client.GetAsync<UserDTO>(DefaultClientEndpoint.Users.List);
            return View(user);
        }

        public async Task<IActionResult> ContactList() 
        {
            var user = await client.GetAsync<UserDTO>(DefaultClientEndpoint.Users.List);
            return View(user);
        }

        [HttpGet]
        public IActionResult ContactAdd() 
        {
            return View(new ContactİnformationDTO());
        }


        [HttpPost]
        public async Task<IActionResult> ContactAdd(ContactİnformationDTO model)
        {
            var response = await client.PostAsync<ContactİnformationDTO, ContactResponse>(DefaultClientEndpoint.Contact.ContactAdd, model);
            if (response?.Success == true) 
            {
                
                return View(model);
            }
            return RedirectToAction("ContactList", "Users");
        }

        [HttpGet]
        public IActionResult ContactUpdate() 
        {
            return View(new ContactİnformationDTO());
        }

        [HttpPost]
        public async Task<IActionResult> ContactUpdate(ContactİnformationDTO model)
        {
            var response = await client.PostAsync<ContactİnformationDTO, ContactResponse>(DefaultClientEndpoint.Contact.ContactUpdate, model);
            if (response?.Success == true) 
            {
                return View(model);
            }
            return RedirectToAction("ContactList", "Users");
        }
    }
}
