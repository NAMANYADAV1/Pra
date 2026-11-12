
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Interfaces;

namespace StudentManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/students
    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
       
        var students = await _studentService.GetAllStudentsAsync();

        return Ok(students);
    }

    // GET: api/students/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);

        if (student == null)
        {
            return NotFound("Student not found");
        }

        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public async Task<IActionResult> AddStudent(StudentRequest request)
    {
        var student = await _studentService.AddStudentAsync(request);

        return CreatedAtAction(
            nameof(GetStudentById),
            new { id = student.Id },
            student
        );
    }

    // PUT: api/students/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStudent(
        int id,
        StudentRequest request)
    {
        var updated = await _studentService.UpdateStudentAsync(id, request);

        if (!updated)
        {
            return NotFound("Student not found");
        }

        return NoContent();
    }

    // DELETE: api/students/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var deleted = await _studentService.DeleteStudentAsync(id);

        if (!deleted)
        {
            return NotFound("Student not found");
        }

        return NoContent();
    }
}
