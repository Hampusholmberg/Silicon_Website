using WebApp.Models.Components;

namespace WebApp.Models.Views;

public class CoursesIndexViewModel
{
    public string Title { get; set; } = "";
    public List<CourseViewModel> Courses { get; set; } = null!;

}
