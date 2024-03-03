namespace Infrastructure.Entities;

public class UserSavedItemEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public UserProfileEntity User { get; set; } = null!;

    public int CourseId { get; set; }
    public CourseEntity Course { get; set; } = null!;
}