
using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken);

    Task<AuthResponse?> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken);
}
