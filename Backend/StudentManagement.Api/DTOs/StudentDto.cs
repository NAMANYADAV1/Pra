
namespace StudentManagement.Api.DTOs;

public sealed class StudentDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public DateTime CreatedAt { get; set; }
}
