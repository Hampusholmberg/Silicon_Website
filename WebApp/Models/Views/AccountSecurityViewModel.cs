using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using System.ComponentModel.DataAnnotations;
using WebApp.Helpers;
using WebApp.Models.Forms;

namespace WebApp.Models.Views;

public class AccountSecurityViewModel
{
    public string? Title { get; set; }
    public PasswordChangeModel? PasswordForm { get; set; }
    public DeleteAccountModel? DeleteAccountForm { get; set; }
    public AccountViewModel? LoggedInUser { get; set; }
}
