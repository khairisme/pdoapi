using HR.Application.DTOs;
using HR.Application.Interfaces.PDO;
using HR.Application.Services;
using HR.Core.Entities;
using HR.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

namespace HR.API.Controllers.PDO;

[ApiController]
[Route("api/pdo/[controller]")]
public class RujJenisSaraanController : ControllerBase
{
    private readonly IRujJenisSaraanService _rujJenisSaraan;
    private readonly ILogger<RujJenisSaraanController> _logger;

    public RujJenisSaraanController(IRujJenisSaraanService rujJenisSaraan, ILogger<RujJenisSaraanController> logger)
    {
        _rujJenisSaraan = rujJenisSaraan;
        _logger = logger;
    }
    /// <summary>
    ///Get all RujJenisSaraan
    /// </summary>
    /// <param name="RujJenisSaraanDto"></param>
    /// <returns></returns>
    [HttpGet("getAll")]
    public async Task<IActionResult> getAll()
    {
        _logger.LogInformation("Getting all RujJenisSaraan");

        var result = await _rujJenisSaraan.GetAllAsync();

        return Ok(new
        {
            status = result.Count() > 0 ? "Sucess" : "Failed",
            items = result

        });
    }


}
