using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Interfaces;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllStudentsAsync();

    Task<StudentDto?> GetStudentByIdAsync(int id);

    Task<StudentDto> AddStudentAsync(StudentRequest request);

    Task<bool> UpdateStudentAsync(int id, StudentRequest request);

    Task<bool> DeleteStudentAsync(int id);
}