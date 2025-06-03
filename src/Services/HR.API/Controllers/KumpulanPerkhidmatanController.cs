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
public class KumpulanPerkhidmatanController : ControllerBase
{
    private readonly IKumpulanPerkhidmatanService _kumpulanPerkhidmatan;
    private readonly ILogger<KumpulanPerkhidmatanController> _logger;

    public KumpulanPerkhidmatanController(IKumpulanPerkhidmatanService kumpulanPerkhidmatan, ILogger<KumpulanPerkhidmatanController> logger)
    {
        _kumpulanPerkhidmatan = kumpulanPerkhidmatan;
        _logger = logger;
    }





    /// <summary>
    ///Get all KumpulanPerkhidmatan
    /// </summary>
    /// <param name="KumpulanPerkhidmatanDto"></param>
    /// <returns></returns>
    [HttpGet("getAll")]
    public async Task<IActionResult> getAll()
    {
        _logger.LogInformation("Getting all KumpulanPerkhidmatanDto");

        var result = await _kumpulanPerkhidmatan.GetAllAsync();

        return Ok(new
        {
            status = result.Count() > 0 ? "Sucess" : "Failed",
            items = result

        });
    }

    /// <summary>
    ///Get Carl Permohonan Perjawatan
    /// </summary>
    /// <param name="KumpulanPerkhidmatanFilterDto"></param>
    /// <returns></returns>
    [HttpPost("getPermohonanPerjawatan")]
    public async Task<IActionResult> getPermohonanPerjawatan([FromBody] KumpulanPerkhidmatanFilterDto filter)
    {
        _logger.LogInformation("Getting all CarlKumpulanPerkhidmatanDto");

        var result = await _kumpulanPerkhidmatan.GetKumpulanPerkhidmatanAsync(filter);

        return Ok(new
        {
            status = result.Count() > 0 ? "Sucess" : "Failed",
            items = result

        });
    }


}
