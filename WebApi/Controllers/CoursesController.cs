using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly CourseRepository _courseRepository;

    public CoursesController(CourseRepository courseRepository)
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        try
        {
            var result = await _courseRepository.GetOneAsync(x => x.Id == id);

            if (result != null!)
                return Ok(result);

            return NotFound();
            
        }
        catch { return Problem(); }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _courseRepository.GetAllAsync();

            if (result != null!)
                return Ok(result);

            return NotFound();
            
        }
        catch { return Problem(); }
    }

    #endregion




    #region UPDATE
    #endregion




    #region DELETE 
    #endregion



}
