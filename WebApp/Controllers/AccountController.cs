using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net;

namespace WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserProfileRepository _userProfileRepository;
        private readonly AddressRepository _addressRepository;


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserProfileRepository UserProfileRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AddressRepository addressRepository)
        {
            _userProfileRepository = UserProfileRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _addressRepository = addressRepository;
        }

        [HttpGet]
        [Route("/account-details")]
        public async Task<IActionResult> AccountDetails()
        {
            var userEmail = User.Identity!.Name;
            var result = await _userProfileRepository.GetOneAsync(x => x.Email == userEmail);

            if (result != null) 
            {
                var userProfile = (UserProfileEntity)result.ContentResult!;

                var viewModel = new AccountViewModel
                {
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName,
                    Email = userProfile.Email,
                    PhoneNumber = userProfile.PhoneNumber,
                    Bio = userProfile.Bio,
                    Address = new AddressViewModel
                    {
                        AddressLine1 = userProfile.Address?.AddressLine1,
                        AddressLine2 = userProfile.Address?.AddressLine2,
                        PostalCode = userProfile.Address?.PostalCode,
                        City = userProfile.Address?.City,
                    },
                    ProfilePicture = new ImageViewModel
                    {
                        ImageUrl = userProfile.ProfilePicture?.ImageUrl
                    }
                };

                return View(viewModel);
            }

            return View();
        }


        // NO ERROR MESSAGES IMPLEMENTED!!
        public async Task<IActionResult> UpdateProfile(AccountViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.Users
                    .Include(x => x.UserProfile)
                    .SingleOrDefaultAsync(x => x.Email == User.Identity!.Name);

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

                    var result = await _userManager.UpdateAsync(user);
                }
            }

            return RedirectToAction("AccountDetails", "Account");
        }


        // NO ERROR MESSAGES IMPLEMENTED!!
        public async Task<IActionResult> UpdateAddress(AccountViewModel viewModel)
        {

            var user = await _userManager.Users
                .Include(x => x.UserProfile)
                .Include(x => x.UserProfile.Address)
                .SingleOrDefaultAsync(x => x.Email == User.Identity!.Name);

            if (user != null)
            {
                AddressEntity address = new AddressEntity
                {
                    AddressLine1 = viewModel.Address.AddressLine1,
                    AddressLine2 = viewModel.Address.AddressLine2,
                    PostalCode = viewModel.Address.PostalCode,
                    City = viewModel.Address.City
                };

                var result = await _addressRepository.ExistsAsync(x => 
                x.AddressLine1 == address.AddressLine1 &&
                x.AddressLine2 == address.AddressLine2 &&
                x.PostalCode == address.PostalCode &&
                x.City == address.City);

                switch (result.Exists!.Value)
                {
                    case false:
                        user.UserProfile.Address = address;
                        await _userManager.UpdateAsync(user);
                        break;

                    case true:
                            var existingAddress = await _addressRepository.GetOneAsync(x =>
                            x.AddressLine1 == address.AddressLine1 &&
                            x.AddressLine2 == address.AddressLine2 &&
                            x.PostalCode == address.PostalCode &&
                            x.City == address.City);
                        if (existingAddress != null)
                        {
                            user.UserProfile.Address = (AddressEntity)existingAddress.ContentResult!;
                            await _userManager.UpdateAsync(user);
                        }
                        break;
                }
            }
            return RedirectToAction("AccountDetails", "Account");
        }


        [Route("/account-security")]
        public IActionResult AccountSecurity()
        {
            AccountViewModel viewModel = new AccountViewModel
            {
                FirstName = "Hampus",
                LastName = "Holmberg",
                Email = "hampus@email.se",
                PhoneNumber = "0701 112 112",
                Bio = "hej hej",
                
                Address = new AddressViewModel
                {
                    AddressLine1 = "Stora Strandgatan 39",
                    AddressLine2 = "",
                    PostalCode = "261 29",
                    City = "Landskrona",
                },

                ProfilePicture = new ImageViewModel { ImageUrl = "/images/people/albert-flores.png" }
            };

            return View(viewModel);
        }


        [Route("/account-saved-items")]
        public async Task<IActionResult> SavedItems()
        {
            AccountViewModel viewModel = new AccountViewModel
            {
                FirstName = "Hampus",
                LastName = "Holmberg",
                Email = "hampus@email.se",
                PhoneNumber = "0701 112 112",
                Bio = "hej hej",

                Address = new AddressViewModel 
                { 
                    AddressLine1 = "Stora Strandgatan 39",
                    AddressLine2 = "",
                    PostalCode = "261 29",
                    City = "Landskrona",
                },

                ProfilePicture = new ImageViewModel { ImageUrl = "/images/people/albert-flores.png" },
                SavedCourses = new List<CourseViewModel>
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
                }
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
