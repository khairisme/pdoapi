using HR.PDO.Application.Interfaces;
using HR.PDO.Core.Entities;
using HR.PDO.Core.Enums;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.Extensions.Logging;
using Shared.Contracts.DTOs;

namespace HR.PDO.Application.Services;

/// <summary>
/// Employee service implementation using Entity Framework Core
/// </summary>
public class EfEmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EfEmployeeService> _logger;

    public EfEmployeeService(
        IUnitOfWork unitOfWork,
        ILogger<EfEmployeeService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<EmployeeDto?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Getting employee by ID {Id} using Entity Framework", id);
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);
        return employee != null ? MapToDto(employee) : null;
    }

    public async Task<EmployeeDto?> GetByEmployeeIdAsync(string employeeId)
    {
        _logger.LogInformation("Getting employee by employee ID {EmployeeId} using Entity Framework", employeeId);
        var repository = _unitOfWork.Repository<Employee>();
        var employees = await repository.FindByFieldAsync("EmployeeId", employeeId);
        var employee = employees.FirstOrDefault();
        return employee != null ? MapToDto(employee) : null;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        _logger.LogInformation("Getting all employees using Entity Framework");
        var employees = await _unitOfWork.Repository<Employee>().GetAllAsync();
        return employees.Select(MapToDto);
    }

    public async Task<IEnumerable<EmployeeDto>> GetByDepartmentAsync(Guid departmentId)
    {
        _logger.LogInformation("Getting employees by department ID {DepartmentId} using Entity Framework", departmentId);
        var repository = _unitOfWork.Repository<Employee>();
        var employees = await repository.FindByFieldAsync("DepartmentId", departmentId);
        return employees.Select(MapToDto);
    }

    public async Task<IEnumerable<EmployeeDto>> GetByManagerAsync(Guid managerId)
    {
        _logger.LogInformation("Getting employees by manager ID {ManagerId} using Entity Framework", managerId);
        var repository = _unitOfWork.Repository<Employee>();
        var employees = await repository.FindByFieldAsync("ManagerId", managerId);
        return employees.Select(MapToDto);
    }

    public async Task<IEnumerable<EmployeeDto>> GetByStatusAsync(EmploymentStatus status)
    {
        _logger.LogInformation("Getting employees by status {Status} using Entity Framework", status);
        var repository = _unitOfWork.Repository<Employee>();
        var employees = await repository.FindByFieldAsync("Status", status);
        return employees.Select(MapToDto);
    }

    public async Task<IEnumerable<EmployeeDto>> SearchByNameAsync(string searchTerm)
    {
        _logger.LogInformation("Searching employees by name with term {SearchTerm} using Entity Framework", searchTerm);
        var repository = _unitOfWork.Repository<Employee>();
        var employees = await repository.SearchByTermAsync(searchTerm);
        return employees.Select(MapToDto);
    }

    public async Task<(IEnumerable<EmployeeDto> Employees, int TotalCount)> GetPagedListAsync(int pageNumber, int pageSize)
    {
        _logger.LogInformation("Getting paged list of employees using Entity Framework: page {PageNumber}, size {PageSize}", pageNumber, pageSize);
        var repository = _unitOfWork.Repository<Employee>();
        var (employees, totalCount) = await repository.GetPagedListAsync(pageNumber, pageSize);
        return (employees.Select(MapToDto), totalCount);
    }

    public async Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto)
    {
        _logger.LogInformation("Creating a new employee using Entity Framework");
        await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            var employee = MapToEntity(employeeDto);
            employee = await _unitOfWork.Repository<Employee>().AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            
            return MapToDto(employee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating employee using Entity Framework");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> UpdateAsync(EmployeeDto employeeDto)
    {
        _logger.LogInformation("Updating employee with ID {Id} using Entity Framework", employeeDto.Id);
        await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            var employee = MapToEntity(employeeDto);
            var result = await _unitOfWork.Repository<Employee>().UpdateAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating employee with ID {Id} using Entity Framework", employeeDto.Id);
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        _logger.LogInformation("Deleting employee with ID {Id} using Entity Framework", id);
        await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            var result = await _unitOfWork.Repository<Employee>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting employee with ID {Id} using Entity Framework", id);
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    private EmployeeDto MapToDto(Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            MiddleName = employee.MiddleName,
            DateOfBirth = employee.DateOfBirth,
            Email = employee.Email,
            Phone = employee.Phone,
            Address = employee.Address,
            JoiningDate = employee.JoiningDate,
            TerminationDate = employee.TerminationDate,
            Status = MapToStatusDto(employee.Status),
            DepartmentId = employee.DepartmentId,
            PositionId = employee.PositionId,
            ManagerId = employee.ManagerId
        };
    }

    private Employee MapToEntity(EmployeeDto dto)
    {
        return new Employee
        {
            Id = dto.Id,
            EmployeeId = dto.EmployeeId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            DateOfBirth = dto.DateOfBirth,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            JoiningDate = dto.JoiningDate,
            TerminationDate = dto.TerminationDate,
            Status = MapToStatus(dto.Status),
            DepartmentId = dto.DepartmentId,
            PositionId = dto.PositionId,
            ManagerId = dto.ManagerId
        };
    }
    
    // Helper methods to map between domain and DTO enums
    private static EmploymentStatusDto MapToStatusDto(EmploymentStatus status)
    {
        return status switch
        {
            EmploymentStatus.Active => EmploymentStatusDto.Active,
            EmploymentStatus.OnLeave => EmploymentStatusDto.OnLeave,
            EmploymentStatus.Suspended => EmploymentStatusDto.Suspended,
            EmploymentStatus.Terminated => EmploymentStatusDto.Terminated,
            EmploymentStatus.Retired => EmploymentStatusDto.Retired,
            _ => EmploymentStatusDto.Active
        };
    }
    
    private static EmploymentStatus MapToStatus(EmploymentStatusDto statusDto)
    {
        return statusDto switch
        {
            EmploymentStatusDto.Active => EmploymentStatus.Active,
            EmploymentStatusDto.OnLeave => EmploymentStatus.OnLeave,
            EmploymentStatusDto.Suspended => EmploymentStatus.Suspended,
            EmploymentStatusDto.Terminated => EmploymentStatus.Terminated,
            EmploymentStatusDto.Retired => EmploymentStatus.Retired,
            _ => EmploymentStatus.Active
        };
    }
}
