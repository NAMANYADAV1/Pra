
namespace StudentManagement.Api.DTOs;

public sealed class CourseResponse
{
    public int Id { get; set; }

    public string CourseName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Fees { get; set; }
}
