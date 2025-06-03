using HR.Core.Enums;

namespace HR.Core.Entities;

/// <summary>
/// Attendance entity representing an employee's daily attendance record
/// </summary>
public class Attendance : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan? CheckInTime { get; set; }
    public TimeSpan? CheckOutTime { get; set; }
    public AttendanceStatus Status { get; set; }
    
    // Navigation properties would be handled in the Infrastructure layer
}
