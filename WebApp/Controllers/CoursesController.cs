using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebApp.Models.Components;
using WebApp.Models.Views;

namespace WebApp.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly string _apiKey = "?key=ZTMwZjkzYzUtMzg2My00MzBlLThiNjItMzU2ZGQ1NTIxMTBi";

        private readonly UserProfileService _userProfileService;
        private readonly CourseService _courseService;
        private readonly IConfiguration _configuration;

        public CoursesController(UserProfileService userProfileService, CourseService courseService, IConfiguration configuration)
        {
            _userProfileService = userProfileService;
            _courseService = courseService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string category = null!)
        {
            await _courseService.RunAsync();

            using var http = new HttpClient();

            var apiUrlCourses = $"https://localhost:7153/api/courses?key={_configuration["ApiKey:Secret"]}";
            var responseCourses = await http.GetAsync(apiUrlCourses);

            var apiUrlCategories = $"https://localhost:7153/api/categories";
            var responseCategories = await http.GetAsync(apiUrlCategories);

            if (responseCourses.IsSuccessStatusCode && responseCategories.IsSuccessStatusCode)
            {
                string jsonContentCourses = await responseCourses.Content.ReadAsStringAsync();
                var courses = JsonConvert.DeserializeObject<IEnumerable<CourseViewModel>>(jsonContentCourses);

                string jsonContentCategories = await responseCategories.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(jsonContentCategories);

                if (!string.IsNullOrEmpty(category))
                {
                    courses = courses!.Where(x => x.CourseCategory.Name == category);
                }

                var userCourses = await _courseService.GetSavedCoursesAsync(User);
                var user = await _userProfileService.GetLoggedInUserAsync(User);

                if (courses != null)
                {
                    foreach (var course in courses)
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
                    Courses = courses!,
                    Categories = categories!,
                    Title = "Courses",
                };

                ViewData["Title"] = viewModel.Title;
                return View(viewModel);
            }

            return View();
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