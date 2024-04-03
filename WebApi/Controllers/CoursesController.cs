using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
//[Authorize(AuthenticationSchemes = "Bearer")]
public class CoursesController : ControllerBase
{
    private readonly CourseRepository _courseRepository;
    private readonly CourseService _courseService;
    private readonly CourseCategoryRepository _courseCategoryRepository;
    private readonly DataContext _context;

    public CoursesController(CourseRepository courseRepository, CourseCategoryRepository courseCategoryRepository, CourseService courseService, DataContext context)
    {
        _courseRepository = courseRepository;
        _courseCategoryRepository = courseCategoryRepository;
        _courseService = courseService;
        _context = context;
    }


    #region CREATE

    [HttpPost]
    public async Task <IActionResult> Create(CourseEntity course)
    {
        if (ModelState.IsValid)
        {
            try
            {
                //var result = await _courseRepository.CreateAsync(course);

                var result = await _courseService.CreateCourse(course);
                if (result != null)
                    return Ok();
            }
            catch { return Problem(); }
        }
        return BadRequest();
    }

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
    public async Task<IActionResult> GetAll(string courseCategory = "", string searchQuery = "", int currentPage = 1, int pageSize = 10)
    {
        try
        {
            #region Query Filters
            var courses = _courseRepository.GetAll();

            var query = courses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(courseCategory))
                query = query.Where(x => x.CourseCategory!.Name.Contains(courseCategory));

            if (!string.IsNullOrWhiteSpace(searchQuery))
                query = query.Where(x => x.Name.Contains(searchQuery) || x.CourseAuthor!.Name.Contains(searchQuery));

            #endregion

            var response = new CourseResponse
            {
                TotalItems = await query.CountAsync(),
            };

            response.TotalPages = (int)Math.Ceiling(response.TotalItems / (double)pageSize);
            response.Courses = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(response);
        }
        catch { return Problem(); }
    }
    
    //[HttpGet]
    //public async Task<IActionResult> GetAll()
    //{
    //    try
    //    {
    //        var result = await _courseRepository.GetAllAsync();
    //        if (result != null!)
    //            return Ok(result);

    //        return NotFound();
    //    }
    //    catch { return Problem(); }
    //}

    #endregion


    #region UPDATE

    [HttpPut]
    public async Task<IActionResult> Update(CourseEntity course)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var result = await _courseRepository.UpdateAsync(x => x.Id == course.Id, course);
                if (result != null)
                    return Ok();
            }
            catch { return Problem(); }
        }
        return BadRequest();
    }

    #endregion


    #region DELETE 

    [HttpDelete]
    public async Task<IActionResult> Delete(CourseEntity course)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var exists = await _courseRepository.ExistsAsync(x => x.Id == course.Id);
                if (exists)
                {
                    var result = _courseRepository.DeleteAsync(x => x.Id == course.Id);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex) { return Problem(); }
        }
        return BadRequest();
    }

    #endregion

}
