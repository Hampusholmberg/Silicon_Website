using Infrastructure.Entities;
using WebApp.Models.Forms;

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
    public string? LikesAmount { get; set; }
    public bool IsSaved { get; set; } = false;
    public AuthorViewModel CourseAuthor { get; set; } = null!;
    public ImageViewModel Image { get; set; } = new ImageViewModel();
    public List<CourseViewModel>? Courses { get; set; }


    public static implicit operator CourseViewModel(CourseEntity Course)
    {
        return new CourseViewModel
        {
            Id = Course.Id,
            CourseName = Course.Name,
            CourseDescription = Course.Description,
            CourseIngress = Course.Ingress,
            Price = Course.Price,
            HoursToComplete = Course.HoursToComplete,
            LikesPercentage = Course.LikesPercentage,
            LikesAmount = Course.LikesAmount,

            Image = new ImageViewModel
            {
                ImageUrl = Course.Image.ImageUrl
            },

            CourseAuthor = new AuthorViewModel
            {
                Name = Course.Author.Name,
                Description = Course.Author.Description,
                YoutubeFollowersQty = Course.Author.YoutubeFollowersQty,
                FacebookFollowersQty = Course.Author.FacebookFollowersQty,

                Image = new ImageViewModel
                {
                    ImageUrl = Course.Author.Image.ImageUrl
                }
            }
        };
    }

}
