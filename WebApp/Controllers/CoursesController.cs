using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebApp.Models.Components;
using WebApp.Models.Views;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly UserProfileService _userProfileService;
        private readonly CourseService _courseService;
        private readonly IConfiguration _configuration;
        private readonly WebAppCourseService _webAppCourseService;

        public CoursesController(UserProfileService userProfileService, CourseService courseService, IConfiguration configuration, WebAppCourseService webAppCourseService)
        {
            _userProfileService = userProfileService;
            _courseService = courseService;
            _configuration = configuration;
            _webAppCourseService = webAppCourseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int CurrentPage = 1, int pageSize = 9)
        {
            await _courseService.RunAsync();

            using var http = new HttpClient();

            var courses = await _webAppCourseService.GetCoursesAsync("", "");
            var categories = await _webAppCourseService.GetCourseCategoriesAsync();


            var userCourses = await _courseService.GetSavedCoursesAsync(User);
            var user = await _userProfileService.GetLoggedInUserAsync(User);

            if (courses != null)
            {
                foreach (var course in courses.Courses)
                {
                    var result = user.UserProfile.SavedItems?.Any(x => x.CourseId == course.Id);
                    if (result == true)
                    {
                        course.IsSaved = true;
                    }
                }
            }

            var viewModel = new CoursesIndexViewModel
            {
                Courses = courses!.Courses,
                Categories = categories!,
                Title = "Courses",
            };
            viewModel.Pagination = new Pagination
            {
                CurrentPage = CurrentPage,
                PageSize = pageSize,
                TotalItems = courses.TotalItems,
                TotalPages = courses.TotalPages
            };

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CoursesIndexViewModel viewModel)
        {
            using var http = new HttpClient();

            var courses = await _webAppCourseService.GetCoursesAsync(viewModel.CourseCategory!, viewModel.SearchQuery!);
            var categories = await _webAppCourseService.GetCourseCategoriesAsync();

            var userCourses = await _courseService.GetSavedCoursesAsync(User);
            var user = await _userProfileService.GetLoggedInUserAsync(User);

            if (courses != null)
            {
                foreach (var course in courses.Courses)
                {
                    var result = user.UserProfile.SavedItems?.Any(x => x.CourseId == course.Id);
                    if (result == true)
                    {
                        course.IsSaved = true;
                    }
                }
            }

            viewModel.Courses = courses!.Courses;
            viewModel.Categories = categories;

            ViewData["Title"] = "Courses";
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            using var http = new HttpClient();
            CourseViewModel viewModel = new();

            var result = await http.GetAsync($"https://localhost:7153/api/courses/{id}/?key={_configuration["ApiKey:Secret"]}");

            if (result.IsSuccessStatusCode)
            {
                string jsonContent = await result.Content.ReadAsStringAsync();
                viewModel = JsonConvert.DeserializeObject<CourseViewModel>(jsonContent)!;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> SaveCourse(CourseViewModel course, CoursesIndexViewModel viewModel)
        {
            if (course != null)
            {
                var result = await _courseService.SaveOrRemoveCourseAsync(course.Id, User);
            }
            return RedirectToAction("index", viewModel);
        }
    }
}