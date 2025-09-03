using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Services;
using HR.PDO.Application.Services.PDO;
using HR.PDO.Core.Entities;
using HR.PDO.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared.Contracts.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.PDO.API.Controllers.PDO;
//[Authorize]
[ApiController]
[Route("api/pdo/v1/[controller]")]
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
    /// <summary>
    /// Get all Kementerian
    /// </summary>
    /// <returns></returns>
    [HttpGet("getKementerian")]
    public async Task<IActionResult> GetKementerian()
    {
        var data = await _unitOrganisasiService.GetUnitOrganisasiByKategoriAsync();
        return Ok(new
        {
            status = data.Count() > 0 ? "Sucess" : "Failed",
            items = data

        });
    }
    /// <summary>
    /// Search UnitOrganisasi by keyword
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return BadRequest("Keyword is required");

        var result = await _unitOrganisasiService.SearchUnitOrganisasiAsync(keyword);
        return Ok(new
        {
            status = result.Count() > 0 ? "Sucess" : "Failed",
            items = result

        });
        
    }

    //Code Added by Amar 17/05/25
    /// <summary>
    /// GetNamaUnitOrganisasi
    /// </summary>
    /// <param name="IdUnitOrganisasi">Filter criteria</param>
    /// <returns>Returns the name of aktiviti organisasi</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpGet("getNamaUnitOrganisasi")]
    public async Task<IActionResult> GetNamaUnitOrganisasi([FromQuery] int IdUnitOrganisasi)
    {
        try
        {
            var result = await _unitOrganisasiService.GetNamaUnitOrganisasi(IdUnitOrganisasi);
            return Ok(new
            {
                status = !string.IsNullOrEmpty(result) ? "Success" : "Failed",
                data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetNamaUnitOrganisasi");
            return StatusCode(500, new
            {
                status = "Error",
                data = string.Empty
            });
        }
    }

    /// <summary>
    /// SetPenjenamaanSemula
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
    public async Task<IActionResult> SetPenjenamaanSemula([FromBody] UnitOrganisasiPenjenamaanSemulaRequestDto penjenamaanSemulaRequestDto)
    {
        _logger.LogInformation("update SetPenjenamaanSemula");


        try
        {
            var isSuccess = await _unitOrganisasiService.SetPenjenamaanSemula(penjenamaanSemulaRequestDto);

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
    //End

    // code added by amar 220725
    [HttpPost("getAgency")]
    public async Task<IActionResult> GetAgency()
    {
        _logger.LogInformation("GetAgency: GetAgency method called from controller");
        try
        {
            var data = await _unitOrganisasiService.GetAgency();

            _logger.LogInformation("GetAgency: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Success" : "Failed",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetAgency: Error occurred in controller while processing request");

            return StatusCode(500, new
            {
                status = "Error",
                items = new List<UnitOrganisasiCarianKetuaPerkhidmatanResponseDto>()
            });
        }
    }


    //end

}
