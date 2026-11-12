
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.DTOs;

public sealed class CourseRequest
{
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string CourseName { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Range(0, 99999999)]
    public decimal Fees { get; set; }
}
