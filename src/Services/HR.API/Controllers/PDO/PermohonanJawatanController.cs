using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Application.Services;
using HR.Core.Entities;
using HR.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
    /// <summary>
    /// GetSenaraiPermohonanJawatan
    /// ----------------------------
    /// Fetches a list of permohonan jawatan (job applications) based on:
    /// - Nombor Rujukan
    /// - Tajuk Permohonan (partial match)
    /// - Kod Status Permohonan
    /// </summary>
    /// <param name="filter">Filtering criteria for permohonan jawatan</param>
    /// <returns>
    /// - 200 OK with list of matched permohonan
    /// - 404 if no record found
    /// - 500 if an error occurs
    /// </returns>
    [HttpPost("GetSenaraiPermohonanJawatan")]
    public async Task<IActionResult> GetSenaraiPermohonanJawatan([FromBody] PermohonanJawatanFilterDto2 filter)
    {
        try
        {
            var result = await _permohonanJawatanService.GetSenaraiPermohonanJawatanAsync(filter);

            return Ok(new
            {
                status = result.Any() ? "Success" : "Not Found",
                items = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetSenaraiPermohonanJawatan");
            return StatusCode(500, new
            {
                status = "Error",
                items = new List<PermohonanJawatanResponseDto>()
            });
        }
    }
    /// <summary>
    /// GetSenaraiPermohonanPindaan
    /// ----------------------------
    /// Fetches a list of permohonan pindaan pengisian jawatan based on optional filters:
    /// - Nombor Rujukan (contains)
    /// - Tajuk Permohonan (contains)
    /// - Kod Status Permohonan (exact)
    /// </summary>
    /// <param name="filter">Filter containing optional fields to search</param>
    /// <returns>
    /// - 200 OK: With filtered list of applications (Success or Not Found)
    /// - 500 Internal Server Error: If any unexpected issue occurs
    /// </returns>
    [HttpPost("GetSenaraiPermohonanPindaan")]
    public async Task<IActionResult> GetSenaraiPermohonanPindaan([FromBody] PermohonanPindaanFilterDto filter)
    {
        try
        {
            var result = await _permohonanJawatanService.GetSenaraiPermohonanPindaanAsync(filter);

            return Ok(new
            {
                status = result.Any() ? "Success" : "Not Found",
                items = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetSenaraiPermohonanPindaan");
            return StatusCode(500, new
            {
                status = "Error",
                items = new List<PermohonanPindaanResponseDto>()
            });
        }
    }

    //Amar Code Start
    /// <summary>
    /// GetSalinanAsa
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpPost("getSalinanAsa")]
    public async Task<IActionResult> GetSalinanAsa([FromBody] SalinanAsaFilterDto filter)
    {
        try
        {
            var result = await _permohonanJawatanService.GetSalinanAsa(filter);

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetSalinanAsa");
            return StatusCode(500, new
            {
                status = "Error",
                items = new List<SalinanAsaResponseDto>()
            });
        }
    }

    /// <summary>
    /// GetSalinanBaharu
    /// </summary>
    /// <param name="IdUnitOrganisasi">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpGet("getSalinanBaharu")]
    public async Task<IActionResult> GetSalinanBaharu([FromQuery] int IdUnitOrganisasi)
    {
        try
        {
            var result = await _permohonanJawatanService.GetSalinanBaharu(IdUnitOrganisasi);

            return Ok(new
            {
                status = result.Any() ? "Success" : "Not Found",
                items = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetSalinanBaharu");
            return StatusCode(500, new
            {
                status = "Error",
                items = new List<SalinanBaharuResponseDto>()
            });
        }
    }

    // One cop API need to be created

    /// <summary>
    /// SetUlasanPasukanPerunding
    /// </summary>
    /// <param name="ulasanPasukanPerundingRequestDto">Request</param>
    /// <returns>Returns message</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpPost("setUlasanPasukanPerunding")]
    public async Task<IActionResult> SetUlasanPasukanPerunding([FromBody] UlasanPasukanPerundingRequestDto ulasanPasukanPerundingRequestDto)
    {
        _logger.LogInformation("update SetUlasanPasukanPerunding");


        try
        {
            var isSuccess = await _permohonanJawatanService.SetUlasanPasukanPerunding(ulasanPasukanPerundingRequestDto);

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

    /// <summary>
    /// GetSenaraiPermohonanPerjawatan
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpPost("getSenaraiPermohonanPerjawatan")]
    public async Task<IActionResult> GetSenaraiPermohonanPerjawatan([FromBody] SenaraiPermohonanPerjawatanFilterDto filter)
    {
        try
        {
            var result = await _permohonanJawatanService.GetSenaraiPermohonanPerjawatan(filter);

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetSenaraiPermohonanPerjawatan");
            return StatusCode(500, new
            {
                status = "Error",
                items = new List<SenaraiPermohonanPerjawatanResponseDto>()
            });
        }
    }

    //Amar Code end
}
