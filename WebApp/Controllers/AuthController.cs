using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Forms;
using WebApp.Models.Views;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        [Route("/signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();
            viewModel.Form = new SignUpModel(); // Initialize the Form property
            return View(viewModel);
        }

        [Route("/signup")]
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel viewModel)
        {
            Console.WriteLine(viewModel.Form.FirstName);
            return View(viewModel);
        }





        [Route("/signin")]
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
