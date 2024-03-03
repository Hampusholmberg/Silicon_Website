using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Components;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserProfileRepository _userProfileRepository;

        public AccountController(UserProfileRepository UserProfileRepository)
        {
            _userProfileRepository = UserProfileRepository;
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
                    AddressLine1 = userProfile.Address?.AddressLine1,
                    AddressLine2 = userProfile.Address?.AddressLine2,
                    PostalCode = userProfile.Address?.PostalCode,
                    City = userProfile.Address?.City,
                };

                return View(viewModel);
            }

            return View();
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
                AddressLine1 = "Stora Strandgatan 39",
                AddressLine2 = "",
                PostalCode = "261 29",
                City = "Landskrona",
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
                AddressLine1 = "Stora Strandgatan 39",
                AddressLine2 = "",
                PostalCode = "261 29",
                City = "Landskrona",
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
