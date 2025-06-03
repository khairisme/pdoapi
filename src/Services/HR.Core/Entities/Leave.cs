using HR.Core.Enums;

namespace HR.Core.Entities;

/// <summary>
/// Leave entity representing an employee's time off request
/// </summary>
public class Leave : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public LeaveType LeaveType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Reason { get; set; }
    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    
    // Navigation properties would be handled in the Infrastructure layer
}
