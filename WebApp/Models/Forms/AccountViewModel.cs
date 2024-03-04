using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using WebApp.Models.Components;

namespace WebApp.Models.Forms;

public class AccountViewModel
{

    [Display(Name = "First name", Prompt = "Enter your first name")]
    [Required(ErrorMessage = "Invalid first name")]
    public string FirstName { get; set; } = null!;


    [Display(Name = "Last name", Prompt = "Enter your last name")]
    [Required(ErrorMessage = "Invalid last name")]
    public string LastName { get; set; } = null!;


    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Enter your email address")]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;


    [Display(Name = "Phone number", Prompt = "Enter your phone number")]
    [Required(ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; }


    [Display(Name = "Bio (optional)", Prompt = "Add a short bio...")]
    public string? Bio { get; set; }


    public int? AddressId { get; set; }


    [Display(Name = "Street name", Prompt = "Enter your street name")]
    public string? AddressLine1 { get; set; }


    [Display(Name = "c/o (optional)", Prompt = "Enter your c/o")]
    public string? AddressLine2 { get; set; }


    [Display(Name = "Postal code", Prompt = "Enter your postal code")]
    public string? PostalCode { get; set; }


    [Display(Name = "City", Prompt = "Enter your city")]
    public string? City { get; set; }

    public ImageViewModel? ProfilePicture { get; set; }
    public List<CourseViewModel>? SavedCourses { get; set; }


    public static implicit operator AccountViewModel(UserProfileEntity userProfile)
    {
        return new AccountViewModel
        {
            FirstName = userProfile.FirstName,
            LastName = userProfile.LastName,
            Email = userProfile.Email,
            PhoneNumber = userProfile.PhoneNumber,
            Bio = userProfile.Bio,
            AddressLine1 = userProfile.Address?.AddressLine1,
            AddressLine2 = userProfile.Address?.AddressLine2,
            PostalCode = userProfile.Address?.PostalCode,
            City = userProfile.Address?.City,
        };
    }








}