using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly CourseRepository _courseRepository;

        public CoursesController(UserProfileService userProfileService, CourseService courseService, CourseRepository courseRepository)
        {
            _userProfileService = userProfileService;
            _courseService = courseService;
            _courseRepository = courseRepository;
        }


        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            await _courseService.RunAsync();

            using var http = new HttpClient();
            var response = await http.GetAsync($"https://localhost:7153/api/courses{_apiKey}");
            int TotalNumberOfCourses = 0;

            if (response != null)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                var courses = JsonConvert.DeserializeObject<IEnumerable<CourseViewModel>>(jsonContent);

                var userCourses = await _courseService.GetSavedCoursesAsync(User);
                var user = await _userProfileService.GetLoggedInUserAsync(User);

                if (courses != null!)
                {
                    TotalNumberOfCourses = courses.Count();

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
                    Title = "Courses",
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalCount = TotalNumberOfCourses,
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
