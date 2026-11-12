
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Interfaces;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/enrollments")]
public sealed class EnrollmentsController(
    IEnrollmentService enrollmentService) : ControllerBase
{
    // GET: api/enrollments
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EnrollmentResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var enrollments = await enrollmentService.GetAllAsync(
            cancellationToken);

        return Ok(enrollments);
    }

    // GET: api/enrollments/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EnrollmentResponse>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var enrollment = await enrollmentService.GetByIdAsync(
            id, cancellationToken);

        if (enrollment is null)
            return NotFound();

        return Ok(enrollment);
    }

    // POST: api/enrollments
    [HttpPost]
    public async Task<ActionResult<EnrollmentResponse>> Create(
        EnrollmentRequest request,
        CancellationToken cancellationToken)
    {
        var enrollment = await enrollmentService.CreateAsync(
            request, cancellationToken);

        if (enrollment is null)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "The specified student or course does not exist."
            });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = enrollment.Id },
            enrollment);
    }

    // PUT: api/enrollments/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EnrollmentResponse>> Update(
        int id,
        EnrollmentRequest request,
        CancellationToken cancellationToken)
    {
        var enrollment = await enrollmentService.UpdateAsync(
            id, request, cancellationToken);

        if (enrollment is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Enrollment was not found, or the specified student or course does not exist."
            });
        }

        return Ok(enrollment);
    }

    // DELETE: api/enrollments/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await enrollmentService.DeleteAsync(
            id, cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
