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
//[Authorize]
[ApiController]
[Route("api/pdo/[controller]")]
public class RujStatusPermohonanController : ControllerBase
{
    private readonly IRujStatusPermohonanService _rujStatusPermohonan;
    private readonly ILogger<RujStatusPermohonanController> _logger;

    public RujStatusPermohonanController(IRujStatusPermohonanService rujStatusPermohonan, ILogger<RujStatusPermohonanController> logger)
    {
        _rujStatusPermohonan = rujStatusPermohonan;
        _logger = logger;
    }





    /// <summary>
    ///Get all KumpulanPerkhidmatan
    /// </summary>
    /// <param name="RujStatusPermohonanDto"></param>
    /// <returns></returns>
    [HttpGet("getAll")]
    public async Task<IActionResult> getAll()
    {
        _logger.LogInformation("Getting all RujStatusPermohonanDto");

        var result = await _rujStatusPermohonan.GetAllAsync();

        return Ok(new
        {
            status = result.Count() > 0 ? "Sucess" : "Failed",
            items = result

        });
    }
    

}
