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
public class AktivitiOrganisasiController : ControllerBase
{
    private readonly IAktivitiOrganisasiService _aktivitiOrganisasiService;
    private readonly ILogger<AktivitiOrganisasiController> _logger;

    public AktivitiOrganisasiController(IAktivitiOrganisasiService aktivitiOrganisasiService, ILogger<AktivitiOrganisasiController> logger)
    {
        _aktivitiOrganisasiService = aktivitiOrganisasiService;
        _logger = logger;
    }

    /// <summary>
    /// Struktur Aktiviti Organisasi
    /// </summary>
    /// <returns></returns>
    [HttpGet("getStrukturAktivitiOrganisasi")]
    public async Task<ActionResult<IEnumerable<AktivitiOrganisasiDto>>> GetAktiviti()
    {
        var data = await _aktivitiOrganisasiService.GetAktivitiOrganisasiAsync();
        return Ok(new
        {
            status = data.Count() > 0 ? "Sucess" : "Failed",
            items = data

        });
    }


}
