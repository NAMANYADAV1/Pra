
namespace StudentManagement.Api.DTOs;

public sealed class AuthResponse
{
    public string Token { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTimeOffset ExpiresAt { get; set; }
}
