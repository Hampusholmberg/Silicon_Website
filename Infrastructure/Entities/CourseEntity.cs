using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CourseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Ingress { get; set; } = null!;

    [DataType("money")]
    public decimal Price { get; set; }
    public int HoursToComplete { get; set; }
    public int LikesPercentage { get; set; }
    public string LikesAmount { get; set; } = null!;


    public int ImageId { get; set; }
    public CourseImageEntity Image { get; set; } = null!;

    public int CourseAuthorId { get; set; }
    public int AuthorId { get; set; }
    public CourseAuthorEntity Author { get; set; } = null!;


}
