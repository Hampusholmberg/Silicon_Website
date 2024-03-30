using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models.Components;
using WebApp.Models.Views;

namespace WebApp.Controllers;

public class AdminController : Controller
{
    private readonly string _apiKey = "?key=ZTMwZjkzYzUtMzg2My00MzBlLThiNjItMzU2ZGQ1NTIxMTBi";

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ManageCourses(ManageCoursesIndexViewModel viewModel)
    {
        using var http = new HttpClient();
        var response = await http.GetAsync($"https://localhost:7153/api/courses{_apiKey}");

        if (response != null!)
        {
            string jsonContent = await response.Content.ReadAsStringAsync();
            var courses = JsonConvert.DeserializeObject<IEnumerable<CourseViewModel>>(jsonContent);

            viewModel.Courses = courses;
        }

        return View(viewModel);
    }

    public async Task<IActionResult> SearchCourse() 
    {
        
        return View("Index");
    } 
}