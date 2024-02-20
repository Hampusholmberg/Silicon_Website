namespace WebApp.Models.Components;

public class CourseViewModel
{
    public int Id { get; set; }
    public string CourseName { get; set; } = null!;
    public string CourseIngress { get; set; } = null!;
    public string CourseDescription { get; set; } = null!;
    public decimal Price { get; set; }
    public int HoursToComplete { get; set; }
    public int? LikesPercentage { get; set; }
    public decimal? LikesAmount { get; set; }
    public AuthorViewModel CourseAuthor { get; set; } = null!;
    public ImageViewModel Image { get; set; } = new ImageViewModel();
}
