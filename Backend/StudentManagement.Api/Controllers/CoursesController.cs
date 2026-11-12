
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Interfaces;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/courses")]
public sealed class CoursesController(ICourseService courseService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CourseResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var courses = await courseService.GetAllAsync(cancellationToken);

        return Ok(courses);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CourseResponse>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var course = await courseService.GetByIdAsync(id, cancellationToken);

        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    [HttpPost]
    public async Task<ActionResult<CourseResponse>> Create(
        CourseRequest request,
        CancellationToken cancellationToken)
    {
        var course = await courseService.CreateAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = course.Id },
            course);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CourseResponse>> Update(
        int id,
        CourseRequest request,
        CancellationToken cancellationToken)
    {
        var course = await courseService.UpdateAsync(
            id,
            request,
            cancellationToken);

        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await courseService.DeleteAsync(id, cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
