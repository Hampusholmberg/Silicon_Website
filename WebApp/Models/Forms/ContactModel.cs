using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Forms;

public class ContactModel
{
    [Display(Name = "Full name", Prompt = "Enter your full name", Order = 0)]
    [Required(ErrorMessage = "Please enter your name")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 1)]
    [Required(ErrorMessage = "Please enter your email address")]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    [Display(Name = "Service (Optional)", Prompt = "Choose the service you are interested in", Order = 2)]
    public string? Service { get; set; }

    [Display(Name = "Message", Prompt = "Enter your message here", Order = 3)]
    [Required(ErrorMessage = "Please enter message")]
    public string Message { get; set; } = null!;





    public List<string> Services { get; set; } = new List<string> 
    {
        "-",
        "Customer Service",
        "Career",
        "Example"
    };
}
