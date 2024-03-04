using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Forms;
using WebApp.Models.Views;

namespace WebApp.Controllers;

public class AuthController : Controller
{

    private readonly UserProfileRepository _userProfileRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(UserProfileRepository UserProfileRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userProfileRepository = UserProfileRepository;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {
        var viewModel = new SignUpViewModel();
        viewModel.Form = new SignUpModel();
        return View(viewModel);
    }

    [Route("/signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser newUser = new ApplicationUser
            {
                FirstName = viewModel.Form.FirstName,
                LastName = viewModel.Form.LastName,
                Email = viewModel.Form.Email,
                UserName = viewModel.Form.Email,

                UserProfile = new UserProfileEntity
                {
                    FirstName = viewModel.Form.FirstName,
                    LastName = viewModel.Form.LastName,
                    Email = viewModel.Form.Email,
                }
            };

            var emailExists = await _userManager.Users.AnyAsync(x => x.Email == newUser.Email);

            if (emailExists)
            {
                viewModel.ErrorMessage = "Email already exists";
                return View(viewModel);
            }
            else
            {
                var result = await _userManager.CreateAsync(newUser, viewModel.Form.Password);
                await _signInManager.PasswordSignInAsync(newUser, viewModel.Form.Password, false, false);
                return RedirectToAction("AccountDetails", "Account");
            }
        }

        viewModel.ErrorMessage = "ERROR";
        return View(viewModel);
    }

    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn()
    {
        var viewModel = new SignInViewModel();
        viewModel.Form = new SignInModel();
        return View(viewModel);
    }

    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, false, false);

            if (result.Succeeded)
                return RedirectToAction("AccountDetails", "Account");   
        }
        viewModel.ErrorMessage = "Invalid email or password";
        return View(viewModel);
    }

    public async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }
}