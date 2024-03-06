using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Components;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Models;
using WebApp.Models.Forms;
using Infrastructure.Services;

namespace WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AddressService _addressService;
        private readonly UserProfileService _userProfileService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, UserProfileService userProfileService, AddressService addressService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userProfileService = userProfileService;
            _addressService = addressService;
        }


        [HttpGet]
        [Route("/account-details")]
        public async Task<IActionResult> AccountDetails()
        {
            if (_signInManager.IsSignedIn(User))
            {
                AccountViewModel viewModel = await _userProfileService.GetLoggedInUserAsync(User);

                return View(viewModel);
            }

            return View();
        }


        // NO ERROR MESSAGES IMPLEMENTED!!
        public async Task<IActionResult> UpdateProfile(AccountViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userProfileService.GetLoggedInUserAsync(User);

                if (user != null)
                {
                    user.FirstName = viewModel.FirstName;
                    user.LastName = viewModel.LastName;
                    user.Email = viewModel.Email;
                    user.UserProfile.FirstName = viewModel.FirstName;
                    user.UserProfile.LastName = viewModel.LastName;
                    user.UserProfile.Email = viewModel.Email;
                    user.UserProfile.PhoneNumber = viewModel.PhoneNumber;
                    user.UserProfile.Bio = viewModel.Bio;

                    await _userManager.UpdateAsync(user);
                }
            }
            return RedirectToAction("AccountDetails", "Account");
        }


        // NO ERROR MESSAGES IMPLEMENTED!!
        public async Task<IActionResult> UpdateAddress(AccountViewModel viewModel)
        {
            var user = await _userProfileService.GetLoggedInUserAsync(User);

            if (user != null)
            {
                user.UserProfile.Address = new AddressEntity
                {
                    AddressLine1 = viewModel.Address.AddressLine1,
                    AddressLine2 = viewModel.Address.AddressLine2,
                    PostalCode = viewModel.Address.PostalCode,
                    City = viewModel.Address.City
                };

                await _addressService.CreateAddressAsync(user);
            }
            return RedirectToAction("AccountDetails", "Account");
        }







        [Route("/account-security")]
        public async Task<IActionResult> AccountSecurity()
        {
            if (_signInManager.IsSignedIn(User))
            {
                AccountViewModel viewModel = await _userProfileService.GetLoggedInUserAsync(User);

                return View(viewModel);
            }

            return View();
        }


        [Route("/account-saved-items")]
        public async Task<IActionResult> SavedItems()
        {
            AccountViewModel viewModel = await _userProfileService.GetLoggedInUserAsync(User);

            viewModel.SavedCourses = new List<CourseViewModel>
            {
                new ()
                {
                    Id = 1,
                    CourseName = "Fullstack Web Developer Course from Scratch",
                    CourseDescription ="Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                    CourseIngress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                    Price = 12.50m,
                    HoursToComplete = 220,
                    LikesPercentage = 94,
                    LikesAmount = 4.2m,
                    Image = new () { ImageUrl = "/images/courses/fullstack-course.png", AltText = ""},
                    CourseAuthor = new() { Id = 1, Name = "Albert Flores", Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.", YoutubeFollowersQty = 240, FacebookFollowersQty = 180, Image = new () { ImageUrl = "/images/people/albert-flores.png", AltText="" } },
                },
                new ()
                {
                    Id = 2,
                    CourseName = "HTML, CSS, JavaScript Web Developer",
                    CourseDescription ="Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                    CourseIngress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                    Price = 15.99m,
                    HoursToComplete = 160,
                    LikesPercentage = 92,
                    LikesAmount = 3.1m,
                    Image = new () { ImageUrl = "/images/courses/frontend-course.png", AltText = ""},
                    CourseAuthor = new() { Id = 1, Name = "Jenny Wilson & Marvin McKinney", Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.", YoutubeFollowersQty = 240, FacebookFollowersQty = 180, Image = new () { ImageUrl = "/images/people/albert-flores.png", AltText="" } },
                },
                new ()
                {
                    Id = 3,
                    CourseName = "The Complete Front-End Web Development Course",
                    CourseDescription ="Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                    CourseIngress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                    Price = 9.99m,
                    HoursToComplete = 100,
                    LikesPercentage = 98,
                    LikesAmount = 2.7m,
                    Image = new () { ImageUrl = "/images/courses/webdev-course.png", AltText = ""},
                    CourseAuthor = new() { Id = 1, Name = "Albert Flores", Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.", YoutubeFollowersQty = 240, FacebookFollowersQty = 180, Image = new () { ImageUrl = "/images/people/albert-flores.png", AltText="" } },
                },
            };

            return View(viewModel);
        }


        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}