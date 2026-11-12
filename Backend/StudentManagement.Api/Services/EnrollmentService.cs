
using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Interfaces;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services;

public sealed class EnrollmentService(ApplicationDbContext context)
    : IEnrollmentService
{
    // Get all enrollments
    public async Task<IReadOnlyList<EnrollmentResponse>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await context.Enrollments
            .AsNoTracking()
            .Select(enrollment => new EnrollmentResponse
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = enrollment.Student.Name,
                CourseId = enrollment.CourseId,
                CourseName = enrollment.Course.CourseName,
                EnrollmentDate = enrollment.EnrollmentDate
            })
            .ToListAsync(cancellationToken);
    }

    // Get enrollment by ID
    public async Task<EnrollmentResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await context.Enrollments
            .AsNoTracking()
            .Where(enrollment => enrollment.Id == id)
            .Select(enrollment => new EnrollmentResponse
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = enrollment.Student.Name,
                CourseId = enrollment.CourseId,
                CourseName = enrollment.Course.CourseName,
                EnrollmentDate = enrollment.EnrollmentDate
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    // Create enrollment
    public async Task<EnrollmentResponse?> CreateAsync(
        EnrollmentRequest request,
        CancellationToken cancellationToken)
    {
        var studentExists = await context.Students
            .AnyAsync(
                student => student.Id == request.StudentId,
                cancellationToken);

        if (!studentExists)
        {
            return null;
        }

        var courseExists = await context.Courses
            .AnyAsync(
                course => course.Id == request.CourseId,
                cancellationToken);

        if (!courseExists)
        {
            return null;
        }

        var enrollment = new Enrollment
        {
            StudentId = request.StudentId,
            CourseId = request.CourseId,
            EnrollmentDate = DateTime.UtcNow
        };

        context.Enrollments.Add(enrollment);

        await context.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(enrollment.Id, cancellationToken);
    }

    // Update enrollment
    public async Task<EnrollmentResponse?> UpdateAsync(
        int id,
        EnrollmentRequest request,
        CancellationToken cancellationToken)
    {
        var enrollment = await context.Enrollments
            .FirstOrDefaultAsync(
                enrollment => enrollment.Id == id,
                cancellationToken);

        if (enrollment is null)
        {
            return null;
        }

        var studentExists = await context.Students
            .AnyAsync(
                student => student.Id == request.StudentId,
                cancellationToken);

        var courseExists = await context.Courses
            .AnyAsync(
                course => course.Id == request.CourseId,
                cancellationToken);

        if (!studentExists || !courseExists)
        {
            return null;
        }

        enrollment.StudentId = request.StudentId;
        enrollment.CourseId = request.CourseId;

        await context.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(id, cancellationToken);
    }

    // Delete enrollment
    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var enrollment = await context.Enrollments
            .FirstOrDefaultAsync(
                enrollment => enrollment.Id == id,
                cancellationToken);

        if (enrollment is null)
        {
            return false;
        }

        context.Enrollments.Remove(enrollment);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
