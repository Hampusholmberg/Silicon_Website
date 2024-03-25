﻿using Infrastructure.Entities;
using Infrastructure.Repositories;
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

    [HttpPost]
    public async Task <IActionResult> Create(CourseEntity course)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var result = await _courseRepository.CreateAsync(course);
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
