using WebApp.Models.Components;
using WebApp.Models.Forms;

namespace WebApp.Models.Views;

public class CoursesIndexViewModel
{
    public string Title { get; set; } = "";
    public IEnumerable<CourseViewModel> Courses { get; set; } = null!;
    public IEnumerable<CategoryViewModel> Categories { get; set; } = null!;
    public AccountViewModel Account { get; set; } = null!;
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; } 
    public string? CourseCategory {  get; set; }
}
