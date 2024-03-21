using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models.Components;
using WebApp.Models.Views;

namespace WebApp.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserProfileRepository _userProfileRepository;
        private readonly UserProfileService _userProfileService;
        private readonly CourseService _courseService;
        private readonly CourseRepository _courseRepository;

        public CoursesController(UserManager<ApplicationUser> userManager, UserProfileRepository userProfileRepository, UserProfileService userProfileService, CourseService courseService, CourseRepository courseRepository)
        {
            _userManager = userManager;
            _userProfileRepository = userProfileRepository;
            _userProfileService = userProfileService;
            _courseService = courseService;
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            await _courseService.RunAsync();

            using var http = new HttpClient();
            var response = await http.GetAsync(@"https://localhost:7153/api/courses");

            if (response != null)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();

                var courses = JsonConvert.DeserializeObject<IEnumerable<CourseViewModel>>(jsonContent);

                var userCourses = await _courseService.GetSavedCoursesAsync(User);
                var user = await _userProfileService.GetLoggedInUserAsync(User);

                foreach (var course in courses)
                {
                    var result = user.UserProfile.SavedItems?.Any(x => x.CourseId == course.Id);
                    if (result == true)
                    {
                        course.IsSaved = true;
                    }
                }

                var viewModel = new CoursesIndexViewModel
                {
                    Courses = courses,
                    Title = "Courses",
                };

                ViewData["Title"] = viewModel.Title;
                return View(viewModel);
            }

            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            List<CourseEntity> coursesEntities = await _courseRepository.GetAllAsync();
            List<CourseViewModel> courses = coursesEntities.Select(course => (CourseViewModel)course).ToList();

            CourseViewModel viewModel = courses[id-1];

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
