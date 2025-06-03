using HR.Application.Interfaces;
using HR.Core.Entities;
using HR.Core.Enums;
using HR.Core.Interfaces;
using Shared.Contracts.DTOs;

namespace HR.Application.Services;

/// <summary>
/// Implementation of the employee service
/// </summary>
public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeDto?> GetByIdAsync(Guid id)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);
        return employee != null ? MapToDto(employee) : null;
    }

    public async Task<EmployeeDto?> GetByEmployeeIdAsync(string employeeId)
    {
        var repository = _unitOfWork.Repository<Employee>();
        var employees = await repository.FindByFieldAsync("EmployeeId", employeeId);
        var employee = employees.FirstOrDefault();
        return employee != null ? MapToDto(employee) : null;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await _unitOfWork.Repository<Employee>().GetAllAsync();
        return employees.Select(MapToDto);
    }

    public async Task<IEnumerable<EmployeeDto>> GetByDepartmentAsync(Guid departmentId)
    {
        var repository = _unitOfWork.Repository<Employee>();
        var employees = await repository.FindByFieldAsync("DepartmentId", departmentId);
        return employees.Select(MapToDto);
    }

    public async Task<IEnumerable<EmployeeDto>> GetByManagerAsync(Guid managerId)
    {
        var repository = _unitOfWork.Repository<Employee>();
        var employees = await repository.FindByFieldAsync("ManagerId", managerId);
        return employees.Select(MapToDto);
    }

    public async Task<IEnumerable<EmployeeDto>> GetByStatusAsync(EmploymentStatus status)
    {
        var repository = _unitOfWork.Repository<Employee>();
        var employees = await repository.FindByFieldAsync("Status", status);
        return employees.Select(MapToDto);
    }

    public async Task<IEnumerable<EmployeeDto>> SearchByNameAsync(string searchTerm)
    {
        var repository = _unitOfWork.Repository<Employee>();
        var employees = await repository.SearchByTermAsync(searchTerm);
        return employees.Select(MapToDto);
    }

    public async Task<(IEnumerable<EmployeeDto> Employees, int TotalCount)> GetPagedListAsync(int pageNumber, int pageSize)
    {
        var repository = _unitOfWork.Repository<Employee>();
        var (employees, totalCount) = await repository.GetPagedListAsync(pageNumber, pageSize);
        return (employees.Select(MapToDto), totalCount);
    }

    public async Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto)
    {
        await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            var employee = MapToEntity(employeeDto);
            employee = await _unitOfWork.Repository<Employee>().AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            
            return MapToDto(employee);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> UpdateAsync(EmployeeDto employeeDto)
    {
        await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            var employee = MapToEntity(employeeDto);
            var result = await _unitOfWork.Repository<Employee>().UpdateAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            
            return result;
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            var result = await _unitOfWork.Repository<Employee>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            
            return result;
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    // Helper methods to map between entity and DTO
    private static EmployeeDto MapToDto(Employee employee)
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

    private static Employee MapToEntity(EmployeeDto dto)
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
