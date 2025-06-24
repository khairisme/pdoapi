using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Application.Services;
using HR.Core.Entities;
using HR.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

namespace HR.API.Controllers.PDO;
//[Authorize]
[ApiController]
[Route("api/pdo/[controller]")]
public class PermohonanJawatanController : ControllerBase
{
    private readonly IPermohonanJawatanService _permohonanJawatanService;
    private readonly ILogger<PermohonanJawatanController> _logger;

    public PermohonanJawatanController(IPermohonanJawatanService permohonanJawatanService, ILogger<PermohonanJawatanController> logger)
    {
        _permohonanJawatanService = permohonanJawatanService;
        _logger = logger;
    }
    /// <summary>
    ///Get Carl PermohonanJawatan
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<PermohonanJawatanSearchResponseDto>>> Search([FromBody] PermohonanJawatanFilterDto filter)
    {
        _logger.LogInformation("search PermohonanJawatan");
        var result = await _permohonanJawatanService.Search(filter).ToListAsync();
        return Ok(new
        {
            status = result.Count() > 0 ? "Sucess" : "Failed",
            items = result

        });
    }
}
