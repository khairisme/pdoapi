using HR.PDO.Application.Interfaces;
using HR.PDO.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

namespace HR.PDO.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Getting all employees");
        
        var (employees, totalCount) = await _employeeService.GetPagedListAsync(pageNumber, pageSize);
        
        return Ok(new
        {
            Items = employees,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting employee with ID: {Id}", id);
        
        var employee = await _employeeService.GetByIdAsync(id);
        
        if (employee == null)
        {
            return NotFound();
        }
        
        return Ok(employee);
    }

    [HttpGet("employeeId/{employeeId}")]
    public async Task<IActionResult> GetByEmployeeId(string employeeId)
    {
        _logger.LogInformation("Getting employee with employee ID: {EmployeeId}", employeeId);
        
        var employee = await _employeeService.GetByEmployeeIdAsync(employeeId);
        
        if (employee == null)
        {
            return NotFound();
        }
        
        return Ok(employee);
    }

    [HttpGet("department/{departmentId:guid}")]
    public async Task<IActionResult> GetByDepartment(Guid departmentId)
    {
        _logger.LogInformation("Getting employees by department ID: {DepartmentId}", departmentId);
        
        var employees = await _employeeService.GetByDepartmentAsync(departmentId);
        
        return Ok(employees);
    }

    [HttpGet("manager/{managerId:guid}")]
    public async Task<IActionResult> GetByManager(Guid managerId)
    {
        _logger.LogInformation("Getting employees by manager ID: {ManagerId}", managerId);
        
        var employees = await _employeeService.GetByManagerAsync(managerId);
        
        return Ok(employees);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(EmploymentStatus status)
    {
        _logger.LogInformation("Getting employees by status: {Status}", status);
        
        var employees = await _employeeService.GetByStatusAsync(status);
        
        return Ok(employees);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string searchTerm)
    {
        _logger.LogInformation("Searching employees with term: {SearchTerm}", searchTerm);
        
        var employees = await _employeeService.SearchByNameAsync(searchTerm);
        
        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto)
    {
        _logger.LogInformation("Creating a new employee");
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var createdEmployee = await _employeeService.CreateAsync(employeeDto);
        
        return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeDto employeeDto)
    {
        _logger.LogInformation("Updating employee with ID: {Id}", id);
        
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
        _logger.LogInformation("Deleting employee with ID: {Id}", id);
        
        var result = await _employeeService.DeleteAsync(id);
        
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
}
