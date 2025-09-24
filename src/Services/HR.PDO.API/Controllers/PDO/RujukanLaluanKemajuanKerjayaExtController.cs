using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Services;
using HR.PDO.Core.Entities;
using HR.PDO.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

namespace HR.PDO.API.Controllers.PDO;
//[Authorize]
[ApiController]
[Route("api/pdo/v1/laluan-kemajuan-kerjaya")]
public class RujukanLaluanKemajuanKerjayaExtController : ControllerBase
{
    private readonly IRujukanLaluanKemajuanKerjayaExt _rujLaluanKemajuanKerjayaExt;
    private readonly ILogger<RujukanLaluanKemajuanKerjayaExtController> _logger;

    public RujukanLaluanKemajuanKerjayaExtController(IRujukanLaluanKemajuanKerjayaExt rujLaluanKemajuanKerjayaExt, ILogger<RujukanLaluanKemajuanKerjayaExtController> logger)
    {
        _rujLaluanKemajuanKerjayaExt = rujLaluanKemajuanKerjayaExt;
        _logger = logger;
    }
    /// <summary>
    ///Get all RujJenisSaraan
    /// </summary>
    /// <param name="RujukanLaluanKemajuanKerja"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> getAll()
    {
        _logger.LogInformation("Getting all RujukanLaluanKemajuanKerja");

        var result = await _rujLaluanKemajuanKerjayaExt.RujukanLaluanKemajuanKerjaya();

        return Ok(new
        {
            status = result.Count() > 0 ? "Berjaya" : "Gagal",
            items = result

        });
    }


}
