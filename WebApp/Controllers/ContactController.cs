using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Views;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ContactViewModel();
            viewModel.Form = new();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(ContactViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
