
using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Interfaces;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services;

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext _context;

    public StudentService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all students
    public async Task<List<StudentDto>> GetAllStudentsAsync()
    {
        return await _context.Students
            .Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                DateOfBirth = s.DateOfBirth,
                CreatedAt = s.CreatedAt
            })
            .ToListAsync();
    }

    // Get student by ID
    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return null;
        }

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Phone = student.Phone,
            DateOfBirth = student.DateOfBirth,
            CreatedAt = student.CreatedAt
        };
    }

    // Add student
    public async Task<StudentDto> AddStudentAsync(StudentRequest request)
    {
        var student = new Student
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            DateOfBirth = request.DateOfBirth!.Value,
            CreatedAt = DateTime.UtcNow
        };

        _context.Students.Add(student);

        await _context.SaveChangesAsync();

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Phone = student.Phone,
            DateOfBirth = student.DateOfBirth,
            CreatedAt = student.CreatedAt
        };
    }

    // Update student
    public async Task<bool> UpdateStudentAsync(
        int id,
        StudentRequest request)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return false;
        }

        student.Name = request.Name;
        student.Email = request.Email;
        student.Phone = request.Phone;
        student.DateOfBirth = request.DateOfBirth!.Value;

        await _context.SaveChangesAsync();

        return true;
    }

    // Delete student
    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return false;
        }

        _context.Students.Remove(student);

        await _context.SaveChangesAsync();

        return true;
    }
}
