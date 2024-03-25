using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.Services;

public class CourseService
{
    private readonly CourseRepository _courseRepository;
    private readonly CourseAuthorRepository _authorRepository;
    private readonly ProfilePictureRepository _pictureRepository;
    private readonly UserProfileService _userProfileService;
    private readonly UserProfileRepository _userProfileRepository;
    private readonly SavedCoursesRepository _savedCoursesRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public CourseService(CourseRepository courseRepository, CourseAuthorRepository authorRepository, ProfilePictureRepository pictureRepository, UserProfileService userProfileService, UserProfileRepository userProfileRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, SavedCoursesRepository savedCoursesRepository)
    {
        _courseRepository = courseRepository;
        _authorRepository = authorRepository;
        _pictureRepository = pictureRepository;
        _userProfileService = userProfileService;
        _userProfileRepository = userProfileRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _savedCoursesRepository = savedCoursesRepository;
    }

    public async Task RunAsync()
    {
        var result = await _courseRepository.GetAllAsync();
        if (result.Count() == 0)
        {
            await PopulateCourseTableAsync();
        }
    }

    public async Task PopulateCourseTableAsync()
    {
        CourseEntity course_1 = new CourseEntity
        {
            Name = "Fullstack Web Developer Course from Scratch",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 12.50m,
            HoursToComplete = 220,
            LikesPercentage = 94,
            LikesAmount = "4.2m",

            Image = new CourseImageEntity
            {
                ImageUrl = "/images/courses/fullstack-course.png"
            },

            Author = new CourseAuthorEntity
            {
                Name = "Albert Flores",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            }

        };
        CourseEntity course_2 = new CourseEntity
        {
            Name = "HTML, CSS, JavaScript Web Developer",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 15.99m,
            HoursToComplete = 160,
            LikesPercentage = 92,
            LikesAmount = "3.1m",

            Image = new CourseImageEntity
            {
                ImageUrl = "/images/courses/frontend-course.png"
            },

            Author = new CourseAuthorEntity
            {
                Name = "Jenny Wilson & Marvin McKinney",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            }
        };
        CourseEntity course_3 = new CourseEntity
        {
            Name = "The Complete Front-End Web Development Course",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 9.99m,
            HoursToComplete = 100,
            LikesPercentage = 98,
            LikesAmount = "2.7m",

            Image = new CourseImageEntity
            {
                ImageUrl = "/images/courses/webdev-course.png"
            },

            Author = new CourseAuthorEntity
            {
                Name = "Albert Flores",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            }
        };

        await _courseRepository.CreateAsync(course_1);
        await _courseRepository.CreateAsync(course_2);
        await _courseRepository.CreateAsync(course_3);


        await _courseRepository.CreateAsync(course_2);
        await _courseRepository.CreateAsync(course_3);
        await _courseRepository.CreateAsync(course_1);

        await _courseRepository.CreateAsync(course_3);
        await _courseRepository.CreateAsync(course_1);
        await _courseRepository.CreateAsync(course_2);
    }


    public async Task<bool> SaveOrRemoveCourseAsync(int id, ClaimsPrincipal loggedInUser)
    {
        var user = await _userProfileService.GetLoggedInUserAsync(loggedInUser);
        if (user != null)
        {
            var savedCourse = new SavedCoursesEntity
            {
                UserProfileId = user.UserProfile.Id,
                CourseId = id
            };

            var exists = await _savedCoursesRepository.ExistsAsync(x => x.CourseId == savedCourse.CourseId && x.UserProfileId == savedCourse.UserProfileId);
            if (!exists)
            {
                user.UserProfile.SavedItems!.Add(savedCourse);
                await _userManager.UpdateAsync(user);
                return true;
            }
            else
            {
                await _savedCoursesRepository.DeleteAsync(x => x.CourseId == savedCourse.CourseId && x.UserProfileId == savedCourse.UserProfileId);
                return true;
            }
        }

        return false;
    }

    public async Task<List<SavedCoursesEntity>> GetSavedCoursesAsync(ClaimsPrincipal loggedInUser)
    {
        var user = await _userProfileService.GetLoggedInUserAsync(loggedInUser);

        if (user != null)
        {
            List<SavedCoursesEntity> savedCourses = [];

            foreach (var savedItem in user.UserProfile.SavedItems!)
            {
                savedCourses.Add(savedItem);
            }
            return savedCourses;
        }

        Console.WriteLine("error");
        return null!;
    }

    public async Task RemoveAllCoursesAssociatedWithUserAsync(ClaimsPrincipal loggedInUser)
    {
        var user = await _userProfileService.GetLoggedInUserAsync(loggedInUser);

        if (user.UserProfile.SavedItems != null)
        {
            foreach (var userCourse in user.UserProfile.SavedItems)
            {
                await _savedCoursesRepository.DeleteAsync(x => x.CourseId == userCourse.CourseId && x.UserProfileId == userCourse.UserProfileId);
            }
        }
    }
}