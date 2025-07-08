using HR.Application.DTOs;
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
[Authorize]
[ApiController]
[Route("api/pdo/[controller]")]
public class UnitOrganisasiController : ControllerBase
{
    private readonly IUnitOrganisasiService _unitOrganisasiService;
    private readonly ILogger<UnitOrganisasiController> _logger;

    public UnitOrganisasiController(IUnitOrganisasiService unitOrganisasiService, ILogger<UnitOrganisasiController> logger)
    {
        _unitOrganisasiService = unitOrganisasiService;
        _logger = logger;
    }

    /// <summary>
    ///Get all UnitOrganisasi
    /// </summary>

    /// <returns></returns>
    [HttpGet("getAll")]
    public async Task<IActionResult> getAll()
    {
        var data = await _unitOrganisasiService.GetAllAsync();
        return Ok(new
        {
            status = data.Count() > 0 ? "Sucess" : "Failed",
            items = data

        });
    }


}
