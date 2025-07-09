using HR.Application.DTOs;
using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Application.Interfaces.PDP;
using HR.Application.Services;
using HR.Core.Entities;
using HR.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

namespace HR.API.Controllers.PDO;

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
            status = result.Count() > 0 ? "Sucess" : "Failed",
            items = result

        });
    }




}
