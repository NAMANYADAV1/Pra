
using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Interfaces;

public interface IEnrollmentService
{
    Task<IReadOnlyList<EnrollmentResponse>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<EnrollmentResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<EnrollmentResponse?> CreateAsync(
        EnrollmentRequest request,
        CancellationToken cancellationToken);

    Task<EnrollmentResponse?> UpdateAsync(
        int id,
        EnrollmentRequest request,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken);
}
