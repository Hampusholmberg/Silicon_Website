namespace Infrastructure.Entities;

public class CourseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int HoursToComplete { get; set; }
    public int? LikesPercentage { get; set; }
    public int? LikesAmount { get; set; }

    public CourseAuthorEntity CourseAuthor { get; set; } = null!;
    public ICollection<UserSavedItemEntity>? Users { get; set; }
}