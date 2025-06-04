using HR.Application.DTOs;
using HR.Application.Interfaces;
using HR.Application.Services;
using HR.Core.Entities;
using HR.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

namespace HR.API.Controllers;

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
   /// <param name="idKlasifikasi"></param>
   /// <param name="idKumpulan"></param>
   /// <returns></returns>
    [HttpGet("getGredList")]
    public async Task<IActionResult> GetGredList([FromQuery] int idKlasifikasi, [FromQuery] int idKumpulan)
    {
        _logger.LogInformation("Getting Carian Gred List");
        if (idKlasifikasi <= 0 || idKumpulan <= 0)
            return BadRequest("Invalid parameters.");

        var data = await _gredService.GetGredListAsync(idKlasifikasi, idKumpulan);
        return Ok(data);
    }

   
}
