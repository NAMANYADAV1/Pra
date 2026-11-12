using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.Models;

public sealed class AppUser
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
