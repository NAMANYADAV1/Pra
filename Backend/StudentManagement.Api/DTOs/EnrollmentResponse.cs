
namespace StudentManagement.Api.DTOs;

public sealed class EnrollmentResponse
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public int CourseId { get; set; }

    public string CourseName { get; set; } = string.Empty;

    public DateTime EnrollmentDate { get; set; }
}
