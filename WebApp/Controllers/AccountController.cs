using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Components;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Models;
using WebApp.Models.Forms;
using Infrastructure.Services;
using Infrastructure.Repositories;

namespace WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AddressService _addressService;
        private readonly UserProfileService _userProfileService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CourseRepository _courseRepository;
        private readonly SavedCoursesRepository _savedCoursesRepository;
        private readonly CourseService _courseService;

        public AccountController(AddressService addressService, UserProfileService userProfileService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CourseRepository courseRepository, SavedCoursesRepository savedCoursesRepository, CourseService courseService)
        {
            _addressService = addressService;
            _userProfileService = userProfileService;
            _userManager = userManager;
            _signInManager = signInManager;
            _courseRepository = courseRepository;
            _savedCoursesRepository = savedCoursesRepository;
            _courseService = courseService;
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


        [Route("/account/SavedItems")]
        public async Task<IActionResult> SavedItems()
        {
            List<CourseEntity> coursesEntities = await _courseRepository.GetAllAsync();
            List<CourseViewModel> courses = coursesEntities.Select(course => (CourseViewModel)course).ToList();

            var user = await _userProfileService.GetLoggedInUserAsync(User);
            

            List<CourseViewModel> coursesToDisplay = [];
            foreach (var course in courses)
            {
                var exists = await _savedCoursesRepository.ExistsAsync(x => x.CourseId == course.Id && x.UserProfileId == user.UserProfile.Id);
                if (exists)
                {
                    coursesToDisplay.Add(course);
                }
            }

            AccountViewModel viewModel = await _userProfileService.GetLoggedInUserAsync(User);

            viewModel.SavedCourses = coursesToDisplay;

            return View(viewModel);
        }

        public async Task<IActionResult> SaveCourse(CourseViewModel course)
        {
            if (course != null)
            {
                var result = await _courseService.SaveCourseAsync(course.Id, User);
            }

            return RedirectToAction("SavedItems", "Account");
        }


        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
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

        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userProfileService.GetLoggedInUserAsync(User);

            if (user != null! )
            {
                await _signInManager.SignOutAsync();
                var result = await _userManager.DeleteAsync(user);
                Console.WriteLine(result);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("AccountSecurity", "Account");
        }
    }
}