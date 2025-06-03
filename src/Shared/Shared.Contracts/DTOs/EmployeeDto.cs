using System;

namespace Shared.Contracts.DTOs;

/// <summary>
/// Data Transfer Object for Employee entity
/// </summary>
public class EmployeeDto
{
    public Guid Id { get; set; }
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
    public EmploymentStatusDto Status { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid PositionId { get; set; }
    public Guid? ManagerId { get; set; }
}

/// <summary>
/// Employment status enum for DTOs
/// </summary>
public enum EmploymentStatusDto
{
    Active,
    OnLeave,
    Suspended,
    Terminated,
    Retired
}
