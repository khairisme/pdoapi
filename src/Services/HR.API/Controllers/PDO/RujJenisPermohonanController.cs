using HR.Application.DTOs;
using HR.Application.Interfaces.PDO;
using HR.Application.Services;
using HR.Core.Entities;
using HR.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

namespace HR.API.Controllers.PDO;
[Authorize]
[ApiController]
[Route("api/pdo/[controller]")]
public class RujJenisPermohonanController : ControllerBase
{
    private readonly IRujStatusPermohonanService _rujStatusPermohonan;
    private readonly ILogger<RujJenisPermohonanController> _logger;

    public RujJenisPermohonanController(IRujStatusPermohonanService rujStatusPermohonan, ILogger<RujJenisPermohonanController> logger)
    {
        _rujStatusPermohonan = rujStatusPermohonan;
        _logger = logger;
    }





    /// <summary>
    ///Get all RujJenisPermohonan
    /// </summary>
    /// <param name="RujJenisPermohonan"></param>
    /// <returns></returns>
    [HttpGet("getAll")]
    public async Task<IActionResult> getAll()
    {
        _logger.LogInformation("Getting all RujJenisPermohonan");

        var result = await _rujStatusPermohonan.GetAllAsync();

        return Ok(new
        {
            status = result.Count() > 0 ? "Sucess" : "Failed",
            items = result

        });
    }


}
