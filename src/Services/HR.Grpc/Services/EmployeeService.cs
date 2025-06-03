using Google.Protobuf;
using Grpc.Core;
using HR.Application.Interfaces;
using HR.Core.Entities;
using HR.Core.Enums;
using HR.Grpc.Protos;

namespace HR.Grpc.Services;

public class EmployeeService : Protos.EmployeeService.EmployeeServiceBase
{
    private readonly ILogger<EmployeeService> _logger;
    private readonly IEmployeeService _employeeService;

    // In a real implementation, you would inject repositories or services
    public EmployeeService(IEmployeeService employeeService, ILogger<EmployeeService> logger)
    {

        _logger = logger; _employeeService = employeeService;

    }

    public override Task<EmployeeResponse> GetEmployee(GetEmployeeRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Getting employee with ID: {EmployeeId}", request.EmployeeId);

        // Mock data for demonstration
        var employee = new EmployeeModel
        {
            Id = Guid.NewGuid().ToString(),
            EmployeeId = request.EmployeeId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "123-456-7890",
            Address = "123 Main St, City, Country",
            JoiningDate = DateTime.UtcNow.AddYears(-2).ToString("o"),
            Status = EmploymentStatusEnum.Active
        };

        return Task.FromResult(new EmployeeResponse { Employee = employee });
    }

    public override async Task<EmployeeListResponse> GetAllEmployees(GetAllEmployeesRequest request, ServerCallContext context)
    {
        try
        {
            _logger.LogInformation("Getting all employees. Page: {Page}, Size: {Size}",
                request.PageNumber, request.PageSize);

            // Create response object first
            var response = new EmployeeListResponse
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = 0 // Will be updated later
            };

            // Get data with detailed logging
            _logger.LogInformation("Calling GetPagedListAsync");
            var employeesResult = await _employeeService.GetPagedListAsync(request.PageNumber, request.PageSize);
            var (employees, totalCount) = employeesResult;
            _logger.LogInformation("Retrieved {Count} employees out of {Total}", employees?.Count() ?? 0, totalCount);

            // Update total count
            response.TotalCount = totalCount;

            // Check if employees is null
            if (employees == null)
            {
                _logger.LogWarning("Employees collection is null");
                return response;
            }

            // Map each employee individually with detailed error handling
            foreach (var employee in employees)
            {
                try
                {
                    _logger.LogDebug("Mapping employee with ID: {Id}", employee.Id);
                    var model = new EmployeeModel
                    {
                        Id = employee.Id.ToString(),
                        EmployeeId = employee.EmployeeId ?? string.Empty,
                        FirstName = employee.FirstName ?? string.Empty,
                        LastName = employee.LastName ?? string.Empty,
                        MiddleName = employee.MiddleName ?? string.Empty,
                        DateOfBirth = employee.DateOfBirth.ToString("o"),
                        Email = employee.Email ?? string.Empty,
                        Phone = employee.Phone ?? string.Empty,
                        Address = employee.Address ?? string.Empty,
                        JoiningDate =  employee.JoiningDate.ToString("o") ,
                        TerminationDate = employee.TerminationDate.HasValue ? employee.TerminationDate.Value.ToString("o") : string.Empty,
                        Status = (EmploymentStatusEnum)(employee.Status),
                        DepartmentId =employee.DepartmentId.ToString() ,
                        PositionId =  employee.PositionId.ToString(),
                        ManagerId = employee.ManagerId.ToString()
                    };
                    response.Employees.Add(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error mapping employee with ID: {Id}", employee.Id);
                    // Continue with next employee instead of failing the entire request
                }
            }

            _logger.LogInformation("Successfully mapped {Count} employees", response.Employees.Count);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in GetAllEmployees method");
            throw; // Re-throw to maintain the original stack trace
        }
    }

    public override Task<EmployeeResponse> CreateEmployee(CreateEmployeeRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Creating new employee: {FirstName} {LastName}",
            request.Employee.FirstName, request.Employee.LastName);

        // In a real implementation, you would save to database
        var employee = request.Employee;
        employee.Id = Guid.NewGuid().ToString();
        employee.EmployeeId = $"EMP{new Random().Next(1000, 9999)}";

        return Task.FromResult(new EmployeeResponse { Employee = employee });
    }

    public override Task<EmployeeResponse> UpdateEmployee(UpdateEmployeeRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Updating employee with ID: {EmployeeId}", request.EmployeeId);

        // In a real implementation, you would update in database
        var employee = request.Employee;

        return Task.FromResult(new EmployeeResponse { Employee = employee });
    }

    public override Task<DeleteEmployeeResponse> DeleteEmployee(DeleteEmployeeRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Deleting employee with ID: {EmployeeId}", request.EmployeeId);

        // In a real implementation, you would delete from database
        return Task.FromResult(new DeleteEmployeeResponse
        {
            Success = true,
            Message = $"Employee with ID {request.EmployeeId} deleted successfully"
        });
    }
}
