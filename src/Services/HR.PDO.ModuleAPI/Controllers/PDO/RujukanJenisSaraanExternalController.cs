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
[Route("api/pdo/v1/external/rujukan/jenis-saraan")]
public class RujukanJenisSaraanExternalController : ControllerBase
{
    private readonly IRujJenisSaraanService _rujJenisSaraan;
    private readonly ILogger<RujukanJenisSaraanExternalController> _logger;

    public RujukanJenisSaraanExternalController(IRujJenisSaraanService rujJenisSaraan, ILogger<RujukanJenisSaraanExternalController> logger)
    {
        _rujJenisSaraan = rujJenisSaraan;
        _logger = logger;
    }
    /// <summary>
    ///Get all RujJenisSaraan
    /// </summary>
    /// <param name="RujJenisSaraanDto"></param>
    /// <returns></returns>
    [HttpGet]
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
