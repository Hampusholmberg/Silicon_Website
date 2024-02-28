namespace WebApp.Models.Dtos;

    public class AddressDto
{
    public int Id { get; set; }
    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}