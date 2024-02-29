namespace Infrastructure.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Bio { get; set; }

    public int AddressId { get; set; }
    public AddressEntity? Address { get; set; }

    public int ProfilePictureId { get; set; }
    public ProfilePictureEntity? ProfilePicture { get; set; }

    public ICollection<UserSavedItemEntity>? SavedItems { get; set; }
}