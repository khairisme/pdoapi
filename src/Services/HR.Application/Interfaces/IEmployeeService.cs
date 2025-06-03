using HR.Core.Enums;
using Shared.Contracts.DTOs;

namespace HR.Application.Interfaces;

/// <summary>
/// Service interface for employee operations
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Get employee by ID
    /// </summary>
    Task<EmployeeDto?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Get employee by employee ID
    /// </summary>
    Task<EmployeeDto?> GetByEmployeeIdAsync(string employeeId);
    
    /// <summary>
    /// Get all employees
    /// </summary>
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    
    /// <summary>
    /// Get employees by department
    /// </summary>
    Task<IEnumerable<EmployeeDto>> GetByDepartmentAsync(Guid departmentId);
    
    /// <summary>
    /// Get employees by manager
    /// </summary>
    Task<IEnumerable<EmployeeDto>> GetByManagerAsync(Guid managerId);
    
    /// <summary>
    /// Get employees by status
    /// </summary>
    Task<IEnumerable<EmployeeDto>> GetByStatusAsync(EmploymentStatus status);
    
    /// <summary>
    /// Search employees by name
    /// </summary>
    Task<IEnumerable<EmployeeDto>> SearchByNameAsync(string searchTerm);
    
    /// <summary>
    /// Get paged list of employees
    /// </summary>
    Task<(IEnumerable<EmployeeDto> Employees, int TotalCount)> GetPagedListAsync(int pageNumber, int pageSize);
    
    /// <summary>
    /// Create a new employee
    /// </summary>
    Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto);
    
    /// <summary>
    /// Update an existing employee
    /// </summary>
    Task<bool> UpdateAsync(EmployeeDto employeeDto);
    
    /// <summary>
    /// Delete an employee
    /// </summary>
    Task<bool> DeleteAsync(Guid id);
}
