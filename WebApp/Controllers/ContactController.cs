using Infrastructure.Repositories;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Views;
using WebApp.Models.Forms;

namespace WebApp.Controllers;

public class ContactController : Controller
{
    private readonly ContactRequestRepository _contactRequestRepository;

    public ContactController(ContactRequestRepository contactRequestRepository)
    {
        _contactRequestRepository = contactRequestRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = new ContactViewModel();
        viewModel.Form = new();

        return View(viewModel);
    }

    [HttpPost]
    public async Task <IActionResult> Index(ContactViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _contactRequestRepository.CreateAsync(viewModel.Form);

            if (result != null) 
            {
                viewModel.SuccessMessage = "Your message has been sent!";
                return View(viewModel);
            }
        }

        viewModel.ErrorMessage = "Something went wrong, your message has not been sent!";
        return View(viewModel);
    }
}
