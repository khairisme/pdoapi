namespace HR.PDO.Core.Enums;

/// <summary>
/// Status of an employee's attendance for a day
/// </summary>
public enum AttendanceStatus
{
    Present = 0,
    Late = 1,
    EarlyDeparture = 2,
    Absent = 3,
    OnLeave = 4,
    Holiday = 5,
    Weekend = 6
}
