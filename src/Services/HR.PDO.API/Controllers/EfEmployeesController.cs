using HR.PDO.Application.Interfaces;
using HR.PDO.Application.Services;
using HR.PDO.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs;

namespace HR.PDO.API.Controllers;

/// <summary>
/// Employee controller using Entity Framework Core implementation
/// </summary>
[ApiController]
[Route("api/ef/[controller]")]
public class EfEmployeesController : ControllerBase
{
    private readonly EfEmployeeService _employeeService;
    private readonly ILogger<EfEmployeesController> _logger;

    public EfEmployeesController(
        EfEmployeeService employeeService,
        ILogger<EfEmployeesController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Getting all employees using Entity Framework Core");
        
        var (employees, totalCount) = await _employeeService.GetPagedListAsync(pageNumber, pageSize);
        
        return Ok(new
        {
            Items = employees,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            DataAccessTechnology = "Entity Framework Core"
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting employee with ID: {Id} using Entity Framework Core", id);
        
        var employee = await _employeeService.GetByIdAsync(id);
        
        if (employee == null)
        {
            return NotFound();
        }
        
        return Ok(new
        {
            Employee = employee,
            DataAccessTechnology = "Entity Framework Core"
        });
    }

    [HttpGet("employeeId/{employeeId}")]
    public async Task<IActionResult> GetByEmployeeId(string employeeId)
    {
        _logger.LogInformation("Getting employee with employee ID: {EmployeeId} using Entity Framework Core", employeeId);
        
        var employee = await _employeeService.GetByEmployeeIdAsync(employeeId);
        
        if (employee == null)
        {
            return NotFound();
        }
        
        return Ok(new
        {
            Employee = employee,
            DataAccessTechnology = "Entity Framework Core"
        });
    }

    [HttpGet("department/{departmentId:guid}")]
    public async Task<IActionResult> GetByDepartment(Guid departmentId)
    {
        _logger.LogInformation("Getting employees by department ID: {DepartmentId} using Entity Framework Core", departmentId);
        
        var employees = await _employeeService.GetByDepartmentAsync(departmentId);
        
        return Ok(new
        {
            Employees = employees,
            DataAccessTechnology = "Entity Framework Core"
        });
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string searchTerm)
    {
        _logger.LogInformation("Searching employees with term: {SearchTerm} using Entity Framework Core", searchTerm);
        
        var employees = await _employeeService.SearchByNameAsync(searchTerm);
        
        return Ok(new
        {
            Employees = employees,
            DataAccessTechnology = "Entity Framework Core",
            SearchTerm = searchTerm
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto)
    {
        _logger.LogInformation("Creating a new employee using Entity Framework Core");
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var createdEmployee = await _employeeService.CreateAsync(employeeDto);
        
        return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, new
        {
            Employee = createdEmployee,
            DataAccessTechnology = "Entity Framework Core"
        });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeDto employeeDto)
    {
        _logger.LogInformation("Updating employee with ID: {Id} using Entity Framework Core", id);
        
        if (id != employeeDto.Id)
        {
            return BadRequest("ID mismatch");
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _employeeService.UpdateAsync(employeeDto);
        
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting employee with ID: {Id} using Entity Framework Core", id);
        
        var result = await _employeeService.DeleteAsync(id);
        
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
