using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Forms;

public class AddressViewModel
{
    public int? AddressId { get; set; }


    [Display(Name = "Street name", Prompt = "Enter your street name")]
    public string AddressLine1 { get; set; } = null!;


    [Display(Name = "c/o (optional)", Prompt = "Enter your c/o")]
    public string? AddressLine2 { get; set; }


    [Display(Name = "Postal code", Prompt = "Enter your postal code")]
    public string? PostalCode { get; set; }


    [Display(Name = "City", Prompt = "Enter your city")]
    public string? City { get; set; }

    public static implicit operator AddressViewModel(UserProfileEntity userProfile)
    {
        return new AddressViewModel
        {
            AddressLine1 = userProfile.Address?.AddressLine1,
            AddressLine2 = userProfile.Address?.AddressLine2,
            PostalCode = userProfile.Address?.PostalCode,
            City = userProfile.Address?.City,
        };
    }
}