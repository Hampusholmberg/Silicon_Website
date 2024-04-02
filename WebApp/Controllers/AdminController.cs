using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebApp.Models.Components;
using WebApp.Models.Views;

namespace WebApp.Controllers;

public class AdminController : Controller
{
    private readonly string _apiKey = "?key=ZTMwZjkzYzUtMzg2My00MzBlLThiNjItMzU2ZGQ1NTIxMTBi";

    private readonly IConfiguration _configuration;

    public AdminController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }


    #region COURSES

    [HttpGet]
    public async Task<IActionResult> ManageCourses()
    {
        return View();
    }

    public async Task<IActionResult> CreateNewCourse(CreateCourseIndexViewModel viewModel)
    {
        viewModel.Title = "Create New Course";
        viewModel.Form = new CourseViewModel();

        return View(viewModel);
    }

    public async Task<IActionResult> CreateCourse(CreateCourseIndexViewModel viewModel)
    {
        if(ModelState.IsValid)
        {
            var course = viewModel.Form;
            using var http = new HttpClient();

            //var token = HttpContext.Request.Cookies["AccessToken"];
            //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var courseAsJson = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");

            var jsonContent = await courseAsJson.ReadAsStringAsync();
            Console.WriteLine(jsonContent);

            //var apiUrl = $"https://localhost:7153/api/courses?key={_configuration["ApiKey:Secret"]}";

            var apiUrl = $"https://localhost:7153/api/courses{_apiKey}";
            var response = await http.PostAsync(apiUrl, courseAsJson);

            if(response.IsSuccessStatusCode)
            {
                viewModel.SuccessMessage = "Course was created successfully";
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);

                viewModel.ErrorMessage = "Something went wrong, course was not created";
            }
        }

        return RedirectToAction("CreateNewCourse", "Admin", viewModel);
    }

    public async Task<IActionResult> UpdateCourse(CreateCourseIndexViewModel viewModel)
    {
        using var http = new HttpClient();
        var response = await http.GetAsync($"https://localhost:7153/api/courses{_apiKey}");

        if (response != null!)
        {
            string jsonContent = await response.Content.ReadAsStringAsync();
            var courses = JsonConvert.DeserializeObject<IEnumerable<CourseViewModel>>(jsonContent);
            viewModel.Courses = courses;
        }

        viewModel.Title = "Update Course";

        return View(viewModel);
    }

    #endregion






    public async Task<IActionResult> SearchCourse() 
    {
        
        return View("Index");
    } 
}