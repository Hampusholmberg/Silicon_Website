using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Services;

public class UserProfileService
{

    private readonly UserManager<ApplicationUser> _userManager;

    public UserProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> GetLoggedInUserAsync(ClaimsPrincipal user)
    {
        if (user.Identity!.IsAuthenticated)
        {
            var loggedInUser = await _userManager.Users
                .Include(x => x.UserProfile)
                .Include(x => x.UserProfile.Address)
                .Include(x => x.UserProfile.ProfilePicture)
                .Include(x => x.UserProfile.SavedItems)
                .FirstOrDefaultAsync(x => x.Email == user.Identity.Name);

            if ( loggedInUser != null! )
            {
                return loggedInUser;
            }
        }

        return null!;
    }
}
