using Grpc.Net.Client;
using HR.Grpc.Protos;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs;

namespace HR.ExternalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    // Mapping methods for employment status
    private EmploymentStatusEnum MapEmploymentStatusToGrpc(EmploymentStatusDto status)
    {
        return status switch
        {
            EmploymentStatusDto.Active => EmploymentStatusEnum.Active,
            EmploymentStatusDto.OnLeave => EmploymentStatusEnum.OnLeave,
            EmploymentStatusDto.Terminated => EmploymentStatusEnum.Terminated,
            EmploymentStatusDto.Retired => EmploymentStatusEnum.Retired,
            _ => EmploymentStatusEnum.Active
        };
    }

    private EmploymentStatusDto MapEmploymentStatusFromGrpc(EmploymentStatusEnum status)
    {
        return status switch
        {
            EmploymentStatusEnum.Active => EmploymentStatusDto.Active,
            EmploymentStatusEnum.OnLeave => EmploymentStatusDto.OnLeave,
            EmploymentStatusEnum.Terminated => EmploymentStatusDto.Terminated,
            EmploymentStatusEnum.Retired => EmploymentStatusDto.Retired,
            _ => EmploymentStatusDto.Active
        };
    }

    private readonly ILogger<EmployeesController> _logger;
    private readonly IConfiguration _configuration;

    public EmployeesController(ILogger<EmployeesController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<EmployeeDto>>> GetEmployees(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Getting all employees from External API");

        // Create a gRPC channel to communicate with the gRPC service
        using var channel = GrpcChannel.ForAddress(_configuration["GrpcSettings:EmployeeServiceUrl"]);
        var client = new EmployeeService.EmployeeServiceClient(channel);

        // Create the request
        var request = new GetAllEmployeesRequest { PageNumber = pageNumber, PageSize = pageSize };

        try
        {
            // Call the gRPC service
            var response = await client.GetAllEmployeesAsync(request);

            // Return the result
            return Ok(new PagedResult<EmployeeDto>
            {
                Items = response.Employees.Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Phone = e.Phone,
                    Address = e.Address,
                    DateOfBirth = DateTime.Parse(e.DateOfBirth) ,
                    JoiningDate =  DateTime.Parse(e.JoiningDate) ,
                    Status = MapEmploymentStatusFromGrpc(e.Status),
                    DepartmentId = Guid.Parse(e.DepartmentId),
                    PositionId = Guid.Parse(e.PositionId),
                    ManagerId = !string.IsNullOrEmpty(e.ManagerId) ? Guid.Parse(e.ManagerId) : null,
                }),
                TotalCount = response.TotalCount,
                PageNumber = response.PageNumber,
                PageSize = response.PageSize,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling gRPC service");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployee(string id)
    {
        _logger.LogInformation("Getting employee with ID: {EmployeeId} from External API", id);

        // Create a gRPC channel to communicate with the gRPC service
        using var channel = GrpcChannel.ForAddress(_configuration["GrpcSettings:EmployeeServiceUrl"]);
        var client = new EmployeeService.EmployeeServiceClient(channel);

        // Create the request
        var request = new GetEmployeeRequest { EmployeeId = id };

        try
        {
            // Call the gRPC service
            var response = await client.GetEmployeeAsync(request);

            // Return the result
            return Ok(new EmployeeDto
            {
                EmployeeId = response.Employee.EmployeeId,
                FirstName = response.Employee.FirstName,
                LastName = response.Employee.LastName,
                Email = response.Employee.Email,
                Phone = response.Employee.Phone,
                Address = response.Employee.Address,
                DateOfBirth = DateTime.Parse(response.Employee.DateOfBirth),
                JoiningDate = DateTime.Parse(response.Employee.JoiningDate),
                Status = MapEmploymentStatusFromGrpc(response.Employee.Status),
                DepartmentId = Guid.Parse(response.Employee.DepartmentId),
                PositionId = Guid.Parse(response.Employee.PositionId),
                ManagerId = !string.IsNullOrEmpty(response.Employee.ManagerId) ? Guid.Parse(response.Employee.ManagerId) : null,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling gRPC service");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] EmployeeDto employeeDto)
    {
        _logger.LogInformation("Creating new employee from External API");

        // Create a gRPC channel to communicate with the gRPC service
        using var channel = GrpcChannel.ForAddress(_configuration["GrpcSettings:EmployeeServiceUrl"]);
        var client = new EmployeeService.EmployeeServiceClient(channel);

        // Map the shared DTO to the gRPC model
        var employeeModel = new EmployeeModel
        {
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            Email = employeeDto.Email,
            Phone = employeeDto.Phone,
            Address = employeeDto.Address,
            DateOfBirth = employeeDto.DateOfBirth.ToString("o") ?? string.Empty,
            JoiningDate = employeeDto.JoiningDate.ToString("o") ?? string.Empty,
            Status = MapEmploymentStatusToGrpc(employeeDto.Status),
            DepartmentId = employeeDto.DepartmentId.ToString(),
            PositionId = employeeDto.PositionId.ToString(),
            ManagerId = !string.IsNullOrEmpty(employeeDto.ManagerId.ToString()) ? employeeDto.ManagerId.ToString() : string.Empty,
        };

        // Create the request
        var request = new CreateEmployeeRequest { Employee = employeeModel };

        try
        {
            // Call the gRPC service
            var response = await client.CreateEmployeeAsync(request);

            // Return the result
            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = response.Employee.EmployeeId },
                new EmployeeDto
                {
                    EmployeeId = response.Employee.EmployeeId,
                    FirstName = response.Employee.FirstName,
                    LastName = response.Employee.LastName,
                    Email = response.Employee.Email,
                    Phone = response.Employee.Phone,
                    Address = response.Employee.Address,
                    DateOfBirth = DateTime.Parse(response.Employee.DateOfBirth) ,
                    JoiningDate = DateTime.Parse(response.Employee.JoiningDate) ,
                    Status = MapEmploymentStatusFromGrpc(response.Employee.Status),
                    DepartmentId = Guid.Parse(response.Employee.DepartmentId),
                    PositionId = Guid.Parse(response.Employee.PositionId),
                    ManagerId = !string.IsNullOrEmpty(response.Employee.ManagerId) ? Guid.Parse(response.Employee.ManagerId) : null,
                });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling gRPC service");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeDto>> UpdateEmployee(string id, [FromBody] EmployeeDto employeeDto)
    {
        _logger.LogInformation("Updating employee with ID: {EmployeeId} from External API", id);

        // Create a gRPC channel to communicate with the gRPC service
        using var channel = GrpcChannel.ForAddress(_configuration["GrpcSettings:EmployeeServiceUrl"]);
        var client = new EmployeeService.EmployeeServiceClient(channel);

        // Map the DTO to the gRPC model
        var employeeModel = new EmployeeModel
        {
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            Email = employeeDto.Email,
            Phone = employeeDto.Phone,
            Address = employeeDto.Address,
            DateOfBirth = employeeDto.DateOfBirth.ToString("o") ?? string.Empty,
            JoiningDate = employeeDto.JoiningDate.ToString("o") ?? string.Empty,
            Status = MapEmploymentStatusToGrpc(employeeDto.Status),
            DepartmentId = employeeDto.DepartmentId.ToString(),
            PositionId = employeeDto.PositionId.ToString(),
            ManagerId = employeeDto.ManagerId?.ToString() ?? string.Empty
        };

        // Create the request
        var request = new UpdateEmployeeRequest { EmployeeId = id, Employee = employeeModel };

        try
        {
            // Call the gRPC service
            var response = await client.UpdateEmployeeAsync(request);

            // Return the result
            return Ok(new EmployeeDto
            {
                EmployeeId = response.Employee.EmployeeId,
                FirstName = response.Employee.FirstName,
                LastName = response.Employee.LastName,
                Email = response.Employee.Email,
                Phone = response.Employee.Phone,
                Address = response.Employee.Address,
                DateOfBirth =  DateTime.Parse(response.Employee.DateOfBirth) ,
                JoiningDate =  DateTime.Parse(response.Employee.JoiningDate) ,
                Status = MapEmploymentStatusFromGrpc(response.Employee.Status),
                DepartmentId = Guid.Parse(response.Employee.DepartmentId),
                PositionId = Guid.Parse(response.Employee.PositionId),
                ManagerId = !string.IsNullOrEmpty(response.Employee.ManagerId) ? Guid.Parse(response.Employee.ManagerId) : null,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling gRPC service");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteEmployee(string id)
    {
        _logger.LogInformation("Deleting employee with ID: {EmployeeId} from External API", id);

        // Create a gRPC channel to communicate with the gRPC service
        using var channel = GrpcChannel.ForAddress(_configuration["GrpcSettings:EmployeeServiceUrl"]);
        var client = new EmployeeService.EmployeeServiceClient(channel);

        // Create the request
        var request = new DeleteEmployeeRequest { EmployeeId = id };

        try
        {
            // Call the gRPC service
            var response = await client.DeleteEmployeeAsync(request);
            return Ok(response.Success);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling gRPC service");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}

// Using the shared DTO from Shared.Contracts

