namespace WebApp.Models.Components;

public class AuthorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ImageViewModel Image { get; set; } = null!;
}
