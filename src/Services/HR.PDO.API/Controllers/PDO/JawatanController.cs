using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Services;
using HR.PDO.Core.Entities;
using HR.PDO.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared.Contracts.DTOs;

namespace HR.PDO.API.Controllers.PDO;
//[Authorize]
[ApiController]
[Route("api/pdo/v1/[controller]")]
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
            status = data.Count() > 0 ? "Berjaya" : "Gagal",
            items = data

        });
    }
    /// <summary>
    /// GetCarianJawatan
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// All filter parameters are optional - if not provided, they will be ignored in the search.
    /// 
    /// </remarks>
    [HttpPost("getCarianJawatan")]
    public async Task<IActionResult> GetCarianJawatan([FromBody] CarianJawatanFilterDto filter)
    {

        _logger.LogInformation("GetCarianJawatan: GetCarianJawatan method called from controller with filter: {@Filter}", filter);
        try
        {
            var data = await _jawatanService.GetCarianJawatanAsync(filter);

            _logger.LogInformation("GetCarianJawatan: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Berjaya" : "Gagal",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetCarianJawatan: Error occurred in controller while processing request with filter: {@Filter}", filter);

            return StatusCode(500, new
            {
                status = "Error",
                items = new List<CarianJawatanResponseDto>()
            });
        }


     
    }
    /// <summary>
    /// getCarianJawatanSebenar
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// All filter parameters are optional - if not provided, they will be ignored in the search.
    /// 
    /// </remarks>
    [HttpPost("getCarianJawatanSebenar")]
    public async Task<IActionResult> GetCarianJawatanSebenar([FromBody] CarianJawatanSebenarFilterDto filter)
    {
        _logger.LogInformation("GetCarianJawatanSebenar: GetCarianJawatanSebenar method called from controller with filter: {@Filter}", filter);
        try
        {
            var data = await _jawatanService.GetCarianJawatanSebenar(filter);

            _logger.LogInformation("GetCarianJawatanSebenar: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Berjaya" : "Gagal",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetCarianJawatanSebenar: Error occurred in controller while processing request with filter: {@Filter}", filter);

            return StatusCode(500, new
            {
                status = "Error",
                items = new List<CarianJawatanSebenarResponseDto>()
            });
        }
    }


    /// <summary>
    /// SearchCarianawatanSebenar
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// All filter parameters are optional - if not provided, they will be ignored in the search.
    /// 
    /// </remarks>
    [HttpPost("searchCarianawatanSebenar")]
    public async Task<IActionResult> SearchCarianawatanSebenar([FromBody] CarianJawatanSebenarReqDto filter)
    {
        _logger.LogInformation("SearchCarianawatanSebenar: SearchCarianawatanSebenarAsync method called from controller with filter: {@Filter}", filter);
        try
        {
            var data = await _jawatanService.SearchCarianawatanSebenarAsync(filter);

            _logger.LogInformation("SearchCarianawatanSebenar: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Berjaya" : "Gagal",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SearchCarianawatanSebenar: Error occurred in controller while processing request with filter: {@Filter}", filter);

            return StatusCode(500, new
            {
                status = "Error",
                items = new List<CarianJawatanSebenarRespDto>()
            });
        }
    }

    // code added by amar 220725

    /// <summary>
    /// GetNamaJawatan
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// All filter parameters are optional - if not provided, they will be ignored in the search.
    /// 
    /// </remarks>
    [HttpGet("GetNamaJawatan")]
    public async Task<IActionResult> GetNamaJawatan([FromQuery] string KodCarta)
    {
        _logger.LogInformation("GetNamaJawatan: Called with KodCarta = {KodCarta}", KodCarta);

        try
        {
            var data = await _jawatanService.GetNamaJawatan(KodCarta);

            _logger.LogInformation("GetNamaJawatan: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Berjaya" : "Gagal",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetNamaJawatan: Error occurred while retrieving data for KodCarta = {KodCarta}", KodCarta);

            return StatusCode(500, new
            {
                status = "Error",
                items = new List<CarianKetuaPerkhidmatanResponseDto>()
            });
        }
    }

    /// <summary>
    /// GetNamaJawatan
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// All filter parameters are optional - if not provided, they will be ignored in the search.
    /// 
    /// </remarks>
    [HttpGet("getSenaraiKetuaPerkhidmatan")]
    public async Task<IActionResult> GetSenaraiKetuaPerkhidmatan([FromQuery] string? NamaJawatan, [FromQuery] string KodCartaOrganisasi)
    {
        _logger.LogInformation("GetSenaraiKetuaPerkhidmatan: Called with NamaJawatan = {NamaJawatan}, KodCartaOrganisasi = {KodCartaOrganisasi}", NamaJawatan, KodCartaOrganisasi);

        try
        {
            var data = await _jawatanService.GetSenaraiKetuaPerkhidmatan(NamaJawatan, KodCartaOrganisasi);

            _logger.LogInformation("GetSenaraiKetuaPerkhidmatan: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Berjaya" : "Gagal",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSenaraiKetuaPerkhidmatan: Error occurred while retrieving data for NamaJawatan = {NamaJawatan}, KodCartaOrganisasi = {KodCartaOrganisasi}", NamaJawatan, KodCartaOrganisasi);

            return StatusCode(500, new
            {
                status = "Error",
                items = new List<SenaraiKetuaPerkhidmatanResponseDto>()
            });
        }
    }


    //end

}
