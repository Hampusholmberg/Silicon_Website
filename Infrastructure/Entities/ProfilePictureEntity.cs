namespace Infrastructure.Entities;

public class ProfilePictureEntity
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? AltText { get; set; }
}