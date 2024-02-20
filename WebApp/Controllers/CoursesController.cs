using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApp.Models.Components;
using WebApp.Models.Views;

namespace WebApp.Controllers
{
    public class CoursesController : Controller
    {


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
                CourseAuthor = new() { Id = 1, Name = "Albert Flores", Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.", Image = new () { ImageUrl = "/images/people/albert-flores.png", AltText="" } },
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
                CourseAuthor = new() { Id = 1, Name = "Jenny Wilson & Marvin McKinney", Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.", Image = new () { ImageUrl = "/images/people/albert-flores.png", AltText="" } },
            },
        };

        public IActionResult Index()
        {
            var viewModel = new CoursesIndexViewModel
            {
                Courses = courses,
                Title = "Courses"
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
