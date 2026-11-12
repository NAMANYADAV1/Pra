
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.DTOs;

public sealed class RegisterRequest
{
    [Required]
    [EmailAddress]
    [StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}
