
using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Interfaces;

public interface ICourseService
{
    Task<IReadOnlyList<CourseResponse>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<CourseResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<CourseResponse> CreateAsync(
        CourseRequest request,
        CancellationToken cancellationToken);

    Task<CourseResponse?> UpdateAsync(
        int id,
        CourseRequest request,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken);
}
