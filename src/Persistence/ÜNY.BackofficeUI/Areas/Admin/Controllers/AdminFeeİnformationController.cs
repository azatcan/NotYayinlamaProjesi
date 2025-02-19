﻿using Microsoft.AspNetCore.Mvc;
using ÜNY.Application.DTOs;
using ÜNY.BackofficeUI.Handlers;
using ÜNY.BackofficeUI.Utils;
using ÜNY.Domain.Entities;
using ÜNY.WebAPI.Model.ContactModel;

namespace ÜNY.BackofficeUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class AdminFeeİnformationController : Controller
    {

        private readonly DefaultClient _client;

        public AdminFeeİnformationController(DefaultClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new FeeİnfoDTO();  
            try
            {
                var users = await _client.GetAsync<List<UserDTO>>(DefaultClientEndpoint.Users.GetAllUsers);
                if (users != null && users.Any())
                {
                    ViewData["Users"] = users ?? new List<UserDTO>();
                }
                else
                {
                    TempData["ErrorMessage"] = "Kullanıcılar alınamadı.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Hata oluştu: " + ex.Message;
                ViewData["Users"] = new List<UserDTO>(); 
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Create(FeeİnfoDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _client.PostAsync<FeeİnfoDTO, ContactResponse>(DefaultClientEndpoint.AdminFeeİnfo.FeeİnfoCreate, model);

                    if (response != null)
                    {
                        TempData["SuccessMessage"] = "Fee information added successfully.";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Fee information could not be added.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error occurred: " + ex.Message;
            }

            return View(model);
        }
    }
}

