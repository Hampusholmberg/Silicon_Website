using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Components;
using WebApp.Models.Views;

namespace WebApp.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserProfileRepository _userProfileRepository;

        public CoursesController(UserManager<ApplicationUser> userManager, UserProfileRepository userProfileRepository)
        {
            _userManager = userManager;
            _userProfileRepository = userProfileRepository;
        }

        public List<CourseViewModel> courses = new List<CourseViewModel>
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

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userProfile = await _userProfileRepository.GetOneAsync(x => x.Email == user.Email);
                user.UserProfile = userProfile;
            }

            var viewModel = new CoursesIndexViewModel
            {
                Courses = courses,
                Title = "Courses",
                User = user!
            };

            ViewData["Title"] = viewModel.Title;

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            CourseViewModel viewModel = courses[id-1];

            return View(viewModel);
        }
    }
}
