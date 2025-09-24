using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Interfaces.PDP;
using HR.PDO.Application.Services;
using HR.PDO.Core.Entities;
using HR.PDO.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

namespace HR.PDO.API.Controllers.PDO;

[ApiController]
[Route("api/pdp/[controller]")]
public class JadualGajiController : ControllerBase
{
    private readonly IJadualGajiService _jadualGajiService;
    private readonly ILogger<JadualGajiController> _logger;

    public JadualGajiController(IJadualGajiService jadualGajiService, ILogger<JadualGajiController> logger)
    {
        _jadualGajiService = jadualGajiService;
        _logger = logger;
    }

    /// <summary>
    ///Get all JadualGaji
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    //[Authorize]
    [HttpGet("getAll")]
    public async Task<IActionResult> getAll()
    {
        _logger.LogInformation("Getting all JadualGaji");

        var result = await _jadualGajiService.GetAllAsync();

        return Ok(new
        {
            status = result.Count() > 0 ? "Berjaya" : "Gagal",
            items = result

        });
    }




}
