using HR.Core.Enums;

namespace HR.Core.Entities;

/// <summary>
/// Employee entity representing a staff member in the organization
/// </summary>
public class Employee : BaseEntity
{
    public string EmployeeId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime JoiningDate { get; set; }
    public DateTime? TerminationDate { get; set; }
    public EmploymentStatus Status { get; set; } = EmploymentStatus.Active;
    public Guid DepartmentId { get; set; }
    public Guid PositionId { get; set; }
    public Guid? ManagerId { get; set; }
    
    // Navigation properties would be handled in the Infrastructure layer
}
