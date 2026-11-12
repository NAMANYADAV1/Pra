using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.DTOs;

public sealed class StudentRequest : IValidatableObject
{
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [StringLength(30)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (DateOfBirth.HasValue &&
            DateOfBirth.Value.Date > DateTime.Today)
        {
            yield return new ValidationResult(
                "Date of birth cannot be in the future.",
                new[] { nameof(DateOfBirth) });
        }
    }
}