using HR.Application.DTOs;
using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Application.Services;
using HR.Application.Services.PDO;
using HR.Core.Entities;
using HR.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;
using System.Diagnostics;

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
    /// <summary>
    /// Wujud Aktiviti Organisasi
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("getAktivitiOrganisasiById/{id}")]
    public async Task<ActionResult<IEnumerable<AktivitiOrganisasiDto>>> GetAktivitiOrganisasiById(int id)
    {
        var data = await _aktivitiOrganisasiService.GetAktivitiOrganisasibyIdAsync(id);
        return Ok(new
        {
            status = data.Count() > 0 ? "Sucess" : "Failed",
            items = data

        });
    }
    /// <summary>
    ///  Simpan  Wujud Aktiviti Organisasi
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>


    [HttpPost("newWujudAktivitiOrganisasi")]
    public async Task<IActionResult> NewWujudAktivitiOrganisasi([FromBody] AktivitiOrganisasiCreateRequest request)
    {
        try
        {
            int newId = await _aktivitiOrganisasiService.SimpanAktivitiAsync(request);
            return Ok(new { message = "Successfully saved", Id = newId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error while saving", error = ex.Message });
        }
    }

    // Amar Code Start
    // <summary>
    /// GetStrukturAktivitiOrganisasi
    /// </summary>
    /// <param name="IdAktivitiOrganisasi">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpGet("getTreeStrukturAktivitiOrganisasi")]
    public async Task<IActionResult> GetTreeStrukturAktivitiOrganisasi([FromQuery] int IdAktivitiOrganisasi)
    {
        try
        {
            var result = await _aktivitiOrganisasiService.GetTreeStrukturAktivitiOrganisasi(IdAktivitiOrganisasi);

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetStrukturAktivitiOrganisasi");
            return StatusCode(500, new
            {
                status = "Error",
                items = new List<StrukturAktivitiOrganisasiResponseDto>()
            });
        }
    }
    /// <summary>
    /// GetNamaAktivitiOrganisasi
    /// </summary>
    /// <param name="IdIndukAktivitiOrganisasi">Filter criteria</param>
    /// <returns>Returns the name of aktiviti organisasi</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpGet("getNamaAktivitiOrganisasi")]
    public async Task<IActionResult> GetNamaAktivitiOrganisasi([FromQuery] int IdIndukAktivitiOrganisasi)
    {
        try
        {
            var result = await _aktivitiOrganisasiService.GetNamaAktivitiOrganisasi(IdIndukAktivitiOrganisasi);
            return Ok(new
            {
                status = !string.IsNullOrEmpty(result) ? "Success" : "Failed",
                data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetNamaAktivitiOrganisasi");
            return StatusCode(500, new
            {
                status = "Error",
                data = string.Empty
            });
        }
    }

    /// <summary>
    /// SetUlasanPasukanPerunding
    /// </summary>
    /// <param name="penjenamaanSemulaRequestDto">Request</param>
    /// <returns>Returns message</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpPost("setPenjenamaanSemula")]
    public async Task<IActionResult> SetPenjenamaanSemula([FromBody] PenjenamaanSemulaRequestDto penjenamaanSemulaRequestDto)
    {
        _logger.LogInformation("update SetPenjenamaanSemula");


        try
        {
            var isSuccess = await _aktivitiOrganisasiService.SetPenjenamaanSemula(penjenamaanSemulaRequestDto);

            if (!isSuccess)
                return StatusCode(500, "Failed to update the record.");

            return Ok("Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during updation");
            return StatusCode(500, ex.InnerException.Message.ToString());
        }
    }

    // <summary>
    /// GetPindahAktivitiOrganisasi
    /// </summary>
    /// <param name="IdAktivitiOrganisasi">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpGet("getPindahAktivitiOrganisasi")]
    public async Task<IActionResult> GetPindahAktivitiOrganisasi([FromQuery] int IdAktivitiOrganisasi)
    {
        try
        {
            var result = await _aktivitiOrganisasiService.GetPindahAktivitiOrganisasi(IdAktivitiOrganisasi);

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetPindahAktivitiOrganisasi");
            return StatusCode(500, new
            {
                status = "Error",
                items = new List<StrukturAktivitiOrganisasiResponseDto>()
            });
        }
    }
    /// <summary>
    /// GetNamaAktivitiOrganisasi
    /// </summary>
    /// <param name="IdIndukAktivitiOrganisasi">Filter criteria</param>
    /// <returns>Returns the name of aktiviti organisasi</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpGet("getNamaKodAktivitiOrganisasi")]
    public async Task<IActionResult> GetNamaKodAktivitiOrganisasi([FromQuery] int IdIndukAktivitiOrganisasi)
    {
        try
        {
            var result = await _aktivitiOrganisasiService.GetNamaKodAktivitiOrganisasi(IdIndukAktivitiOrganisasi);
            return Ok(new
            {
                status = result != null ? "Success" : "Failed",
                data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetNamaKodAktivitiOrganisasi");
            return StatusCode(500, new
            {
                status = "Error",
                data = new { KodAktivitiOrganisasi = string.Empty, NamaAktivitiOrganisasi = string.Empty }
            });
        }
    }

    /// <summary>
    /// SetAktivitiOrganisasi
    /// </summary>
    /// <param name="aktivitiOrganisasiRequestDto">Request</param>
    /// <returns>Returns message</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// 
    /// 
    /// </remarks>
    /// 
    [HttpPost("setAktivitiOrganisasi")]
    public async Task<IActionResult> SetAktivitiOrganisasi([FromBody] AktivitiOrganisasiRequestDto aktivitiOrganisasiRequestDto)
    {
        _logger.LogInformation("update SetPenjenamaanSemula");


        try
        {
            var isSuccess = await _aktivitiOrganisasiService.SetAktivitiOrganisasi(aktivitiOrganisasiRequestDto);

            if (!isSuccess)
                return StatusCode(500, "Failed to update the record.");

            return Ok("Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during updation");
            return StatusCode(500, ex.InnerException.Message.ToString());
        }
    }

    //Amar Code End


}
