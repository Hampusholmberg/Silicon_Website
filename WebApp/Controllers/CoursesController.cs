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
        private readonly string _apiKey = "?key=ZTMwZjkzYzUtMzg2My00MzBlLThiNjItMzU2ZGQ1NTIxMTBi";

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
        public async Task<IActionResult> Index(int pageSize = 10, int CurrentPage = 1)
        {
            await _courseService.RunAsync();

            using var http = new HttpClient();

            var courses = await _webAppCourseService.GetCoursesAsync("");
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

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CoursesIndexViewModel viewModel, string courseCategory = "", string courseQuery = "", int pageSize = 10, int CurrentPage = 1)
        {
            await _courseService.RunAsync();

            using var http = new HttpClient();

            var courses = await _webAppCourseService.GetCoursesAsync(viewModel.CourseCategory!);
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

            var result = await http.GetAsync($"https://localhost:7153/api/courses/{id}/{_apiKey}");

            if (result.IsSuccessStatusCode)
            {
                string jsonContent = await result.Content.ReadAsStringAsync();
                viewModel = JsonConvert.DeserializeObject<CourseViewModel>(jsonContent)!;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> SaveCourse(CourseViewModel course)
        {
            if (course != null)
            {
                var result = await _courseService.SaveOrRemoveCourseAsync(course.Id, User);
            }
            return RedirectToAction("Index", "Courses");
        }
    }
}