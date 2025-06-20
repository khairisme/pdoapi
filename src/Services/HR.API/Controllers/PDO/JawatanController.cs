using HR.Application.DTOs;
using HR.Application.DTOs.PDO;
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
public class JawatanController : ControllerBase
{
    private readonly IJawatanService _jawatanService;
    private readonly ILogger<JawatanController> _logger;

    public JawatanController(IJawatanService jawatanService, ILogger<JawatanController> logger)
    {
        _jawatanService = jawatanService;
        _logger = logger;
    }

    /// <summary>
    /// Get all Jawatan with Agensi
    /// </summary>
    /// <param name="namaJwtn"></param>
    /// <param name="kodCartaOrganisasi"></param>
    /// <returns></returns>

    [HttpGet("searchJawatan")]
    public async Task<IActionResult> SearchJawatan([FromQuery] string namaJwtn, [FromQuery] string kodCartaOrganisasi)
    {
        var data = await _jawatanService.GetJawatanWithAgensiAsync(namaJwtn ?? "", kodCartaOrganisasi ?? "");
        return Ok(new
        {
            status = data.Count() > 0 ? "Sucess" : "Failed",
            items = data

        });
    }
    


}
