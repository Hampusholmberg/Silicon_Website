using WebApp.Models.Components;
using WebApp.Models.Forms;

namespace WebApp.Models.Views;

public class ManageCoursesIndexViewModel
{
    public string Title { get; set; } = "";
    public IEnumerable<CourseViewModel> Courses { get; set; } = null!;
}
