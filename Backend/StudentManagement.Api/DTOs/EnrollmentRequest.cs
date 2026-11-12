
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.DTOs;

public sealed class EnrollmentRequest
{
    [Range(1, int.MaxValue)]
    public int StudentId { get; set; }

    [Range(1, int.MaxValue)]
    public int CourseId { get; set; }
}
