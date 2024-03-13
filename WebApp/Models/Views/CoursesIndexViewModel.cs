using Infrastructure.Models;
using WebApp.Models.Components;
using WebApp.Models.Forms;

namespace WebApp.Models.Views;

public class CoursesIndexViewModel
{
    public string Title { get; set; } = "";
    public List<CourseViewModel> Courses { get; set; } = null!;
    public AccountViewModel Account { get; set; } = null!;
}
