using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.Models;

public sealed class Course
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string CourseName { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Range(0, 99999999)]
    public decimal Fees { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
