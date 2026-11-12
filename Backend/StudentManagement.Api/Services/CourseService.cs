
using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Interfaces;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services;

public sealed class CourseService(ApplicationDbContext context) : ICourseService
{
    // Get all courses
    public async Task<IReadOnlyList<CourseResponse>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await context.Courses
            .AsNoTracking()
            .Select(course => new CourseResponse
            {
                Id = course.Id,
                CourseName = course.CourseName,
                Description = course.Description,
                Fees = course.Fees
            })
            .ToListAsync(cancellationToken);
    }

    // Get course by ID
    public async Task<CourseResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await context.Courses
            .AsNoTracking()
            .Where(course => course.Id == id)
            .Select(course => new CourseResponse
            {
                Id = course.Id,
                CourseName = course.CourseName,
                Description = course.Description,
                Fees = course.Fees
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    // Create course
    public async Task<CourseResponse> CreateAsync(
        CourseRequest request,
        CancellationToken cancellationToken)
    {
        var course = new Course
        {
            CourseName = request.CourseName,
            Description = request.Description,
            Fees = request.Fees
        };

        context.Courses.Add(course);

        await context.SaveChangesAsync(cancellationToken);

        return new CourseResponse
        {
            Id = course.Id,
            CourseName = course.CourseName,
            Description = course.Description,
            Fees = course.Fees
        };
    }

    // Update course
    public async Task<CourseResponse?> UpdateAsync(
        int id,
        CourseRequest request,
        CancellationToken cancellationToken)
    {
        var course = await context.Courses
            .FirstOrDefaultAsync(
                course => course.Id == id,
                cancellationToken);

        if (course is null)
        {
            return null;
        }

        course.CourseName = request.CourseName;
        course.Description = request.Description;
        course.Fees = request.Fees;

        await context.SaveChangesAsync(cancellationToken);

        return new CourseResponse
        {
            Id = course.Id,
            CourseName = course.CourseName,
            Description = course.Description,
            Fees = course.Fees
        };
    }

    // Delete course
    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var course = await context.Courses
            .FirstOrDefaultAsync(
                course => course.Id == id,
                cancellationToken);

        if (course is null)
        {
            return false;
        }

        context.Courses.Remove(course);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
