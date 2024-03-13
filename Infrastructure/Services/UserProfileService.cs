using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Services;

public class UserProfileService
{

    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UserProfileRepository _userProfileRepository;

    public UserProfileService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, UserProfileRepository userProfileRepository)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _userProfileRepository = userProfileRepository;
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
