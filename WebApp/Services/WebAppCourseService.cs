using Infrastructure.Services;
using Newtonsoft.Json;
using WebApp.Models.Components;
using WebApp.Models.Views;

namespace WebApp.Services;

public class WebAppCourseService
{

    private readonly IConfiguration _configuration;

    public WebAppCourseService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<CoursesIndexViewModel> GetCoursesAsync(string courseCategory, string searchQuery)
    {
        using var http = new HttpClient();

        var apiUrl = $"https://localhost:7153/api/courses?" +
            $"courseCategory={courseCategory}&" +
            $"searchQuery={searchQuery}&" +
            //$"currentPage={currentPage}&" +
            //$"pageSize={pageSize}&" +
            $"key={_configuration["ApiKey:Secret"]}";

        var response = await http.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string jsonContentCourses = await response.Content.ReadAsStringAsync();
            var courses = JsonConvert.DeserializeObject<CoursesIndexViewModel>(jsonContentCourses);

            return courses!;
        }
        return null!;
    }


    public async Task<IEnumerable<CategoryViewModel>> GetCourseCategoriesAsync()
    {
        using var http = new HttpClient();

        var apiUrl = $"https://localhost:7153/api/categories";
        var response = await http.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string jsonContentCategories = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(jsonContentCategories);

            return categories!;
        }
        return null!;
    }








}
