using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscribersController : ControllerBase
{
    private readonly CourseRepository _courseRepository;

    public SubscribersController(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }



    #region CREATE

    //[HttpPost]
    //public IActionResult Create()
    //{

    //    return Ok();
    //}

    #endregion




    #region READ
    #endregion




    #region UPDATE
    #endregion




    #region DELETE 
    #endregion



}
