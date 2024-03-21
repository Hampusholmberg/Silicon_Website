using Infrastructure.Entities;

namespace WebApp.Models.Components;

public class CourseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public decimal Price { get; set; }
    public int HoursToComplete { get; set; }
    public int? LikesPercentage { get; set; }
    public string? LikesAmount { get; set; }
    public bool IsSaved { get; set; } = false;
    public AuthorViewModel Author { get; set; } = new AuthorViewModel();
    public ImageViewModel Image { get; set; } = new ImageViewModel();
    public List<CourseViewModel>? Courses { get; set; }


    public static implicit operator CourseViewModel(CourseEntity course)
    {
        return new CourseViewModel
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            Ingress = course.Ingress,
            Price = course.Price,
            HoursToComplete = course.HoursToComplete,
            LikesPercentage = course.LikesPercentage,
            LikesAmount = course.LikesAmount,

            Image = new ImageViewModel
            {
                ImageUrl = course.Image.ImageUrl
            },

            Author = new AuthorViewModel
            {
                Id = course.AuthorId,
                Name = course.Author.Name,
                Description = course.Author.Description,
                YoutubeFollowersQty = course.Author.YoutubeFollowersQty,
                FacebookFollowersQty = course.Author.FacebookFollowersQty,

                Image = new ImageViewModel
                {
                    ImageUrl = course.Author.Image.ImageUrl
                }
            }
        };
    }

}
