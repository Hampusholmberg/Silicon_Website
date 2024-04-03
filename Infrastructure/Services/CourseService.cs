using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Infrastructure.Services;

public class CourseService
{
    private readonly CourseRepository _courseRepository;
    private readonly CourseCategoryRepository _courseCategoryRepository;
    private readonly CourseAuthorRepository _courseAuthorRepository;
    private readonly ProfilePictureRepository _pictureRepository;
    private readonly UserProfileService _userProfileService;
    private readonly UserProfileRepository _userProfileRepository;
    private readonly SavedCoursesRepository _savedCoursesRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public CourseService(CourseRepository courseRepository, CourseCategoryRepository courseCategoryRepository, CourseAuthorRepository courseAuthorRepository, ProfilePictureRepository pictureRepository, UserProfileService userProfileService, UserProfileRepository userProfileRepository, SavedCoursesRepository savedCoursesRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _courseRepository = courseRepository;
        _courseCategoryRepository = courseCategoryRepository;
        _courseAuthorRepository = courseAuthorRepository;
        _pictureRepository = pictureRepository;
        _userProfileService = userProfileService;
        _userProfileRepository = userProfileRepository;
        _savedCoursesRepository = savedCoursesRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
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
        List<CourseEntity> courses = new List<CourseEntity>
        {
            new CourseEntity
        {
            Name = "Fullstack Web Developer Course from Scratch",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 12.50m,
            HoursToComplete = 220,
            LikesPercentage = 94,
            LikesAmount = 4200,

            Image = new CourseImageEntity
            {
                ImageUrl = "/images/courses/course-1.png"
            },

            CourseAuthor = new CourseAuthorEntity
            {
                Name = "Albert Flores",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            },
            CourseCategory = new CourseCategoryEntity
            {
                Name = "Fullstack Development"
            }
        },

            new CourseEntity
        {
            Name = "HTML, CSS, JavaScript Web Developer",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 15.99m,
            HoursToComplete = 160,
            LikesPercentage = 92,
            LikesAmount = 3100,

            Image = new CourseImageEntity
            {
                ImageUrl = "/images/courses/course-2.png"
            },

            CourseAuthor = new CourseAuthorEntity
            {
                Name = "Jenny Wilson & Marvin McKinney",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            },
            CourseCategory = new CourseCategoryEntity
            {
                Name = "Frontend Development"
            }
        },

            new CourseEntity
        {
            Name = "The Complete Front-End Web Development Course",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 9.99m,
            HoursToComplete = 100,
            LikesPercentage = 98,
            LikesAmount = 2700,

            Image = new CourseImageEntity
            {
                ImageUrl = "/images/courses/course-3.png"
            },

            CourseAuthor = new CourseAuthorEntity
            {
                Name = "Albert Flores",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            },
            CourseCategoryId = 2
        },

            new CourseEntity
        {
            Name = "iOS & Swift - The Complete iOS App Development Course",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 15.99m,
            HoursToComplete = 160,
            LikesPercentage = 92,
            LikesAmount = 3100,

            Image = new CourseImageEntity
            {
                ImageUrl = "/images/courses/course-4.png"
            },

            CourseAuthor = new CourseAuthorEntity
            {
                Name = "Marvin McKinney",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            },
            CourseCategory = new CourseCategoryEntity
            {
                Name = "App Development"
            }

        },

            new CourseEntity
        {
            Name = "Data Science & Machine Learning with Python",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 11.20m,
            HoursToComplete = 160,
            LikesPercentage = 92,
            LikesAmount = 3100,

            Image = new CourseImageEntity
            {
                ImageUrl = "/images/courses/course-5.png"
            },

            CourseAuthor = new CourseAuthorEntity
            {
                Name = "Esther Howard",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            },
            CourseCategory = new CourseCategoryEntity
            {
                Name = "AI Development"
            }


        },

            new CourseEntity
            {
                Name = "Creative CSS Drawing Course: Make Art With CSS",
                Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                Price = 10.50m,
                HoursToComplete = 220,
                LikesPercentage = 94,
                LikesAmount = 4200,

                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-6.png"
                },

                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Robert Fox",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
                CourseCategoryId = 2

            },

            new CourseEntity
            {
                Name = "Blender Character Creator v2.0 for Video Games Design",
                Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                Price = 18.99m,
                HoursToComplete = 160,
                LikesPercentage = 92,
                LikesAmount = 3100,

                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-7.png"
                },

                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Ralph Edwards",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
                CourseCategory = new CourseCategoryEntity
                {
                    Name = "Game Development"
                }
            },

            new CourseEntity
            {
                Name = "The Ultimate Guide to 2D Mobile Game Development with Unity",
                Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                Price = 44.99m,
                HoursToComplete = 100,
                LikesPercentage = 98,
                LikesAmount = 2700,

                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-8.png"
                },

                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Albert Flores",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
                CourseCategoryId = 5
            },

            new CourseEntity
            {
                Name = "Learn JMETER from Scratch on Live Apps-Performance Testing",
                Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                Price = 14.50m,
                HoursToComplete = 160,
                LikesPercentage = 92,
                LikesAmount = 3100,

                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-9.png"
                },

                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Jenny Wilson",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
                CourseCategoryId = 3
            },
        };

        foreach (var course in courses)
            await _courseRepository.CreateAsync(course);
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

    public async Task<CourseEntity> CreateCourse(CourseEntity course)
    {
        var courseAuthor = course.CourseAuthor;
        var courseCategory = course.CourseCategory;

        #region AUTHOR CHECK

        if (courseAuthor != null)
        {
            var authorResult = await _courseAuthorRepository.ExistsAsync(x => 
                x.Name == courseAuthor.Name);

            switch (authorResult)
            {
                case true:
                    courseAuthor = await _courseAuthorRepository.GetOneAsync(x => x.Name == courseAuthor.Name);
                    course.CourseAuthorId = courseAuthor.Id;
                    course.CourseAuthor = null!;
                    break;

                case false:
                    var result = await _courseAuthorRepository.CreateAsync(courseAuthor);
                    course.CourseAuthorId = result.Id;
                    course.CourseAuthor = null!;
                    break;
            }
        }

        #endregion


        #region CATEGORY CHECK

        if (courseCategory != null)
        {
            var categoryResult = await _courseCategoryRepository.ExistsAsync(x => 
                x.Name == courseCategory.Name);

            switch (categoryResult)
            {
                case true:
                    courseCategory = await _courseCategoryRepository.GetOneAsync(x => x.Name == courseCategory.Name);
                    course.CourseCategoryId = courseCategory.Id;
                    course.CourseCategory = null!;

                    break;

                case false:
                    var result = await _courseCategoryRepository.CreateAsync(courseCategory);
                    course.CourseCategoryId = result.Id;
                    course.CourseCategory = null!;
                    break;
            }
        }

        #endregion

        var created = await _courseRepository.CreateAsync(course);

        return created;
    } 
}