using HR.Application.DTOs;
using HR.Application.DTOs.PDO;
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
public class GredController : ControllerBase
{
    private readonly IGredService _gredService;
    private readonly ILogger<GredController> _logger;

    public GredController(IGredService gredService, ILogger<GredController> logger)
    {
        _gredService = gredService;
        _logger = logger;
    }





    /// <summary>
    /// Carian Gred
    /// </summary>
    
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("getGredList")]
    public async Task<IActionResult> GetGredList([FromBody] GredFilterDto filter)
    {
        _logger.LogInformation("Getting Carian Gred List");
        

        var data = await _gredService.GetGredListAsync(filter);
        return Ok(new
        {
            status = data.Count() > 0 ? "Sucess" : "Failed",
            items = data

        });
    }
    /// <summary>
    /// Search Maklumat Gred
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("getMaklumatGred")]
    public async Task<IActionResult> GetMaklumatGred([FromBody] GredFilterDto filter)
    {
        var result = await _gredService.GetFilteredGredList(filter);
        return Ok(new
        {
            status = result.Count() > 0 ? "Sucess" : "Failed",
            items = result

        });
        
    }


}
