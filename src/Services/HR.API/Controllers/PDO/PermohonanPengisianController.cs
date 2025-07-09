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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.API.Controllers.PDO;
[Authorize]
[ApiController]
[Route("api/pdo/[controller]")]
public class PermohonanPengisianController : ControllerBase
{
    private readonly IPermohonanPengisianService _service;
    private readonly ILogger<PermohonanPengisianController> _logger;

    public PermohonanPengisianController(IPermohonanPengisianService service, ILogger<PermohonanPengisianController> logger)
    {
        _service = service;
        _logger = logger;
    }





    /// <summary>
    /// Permohonan Pengisian Jawatan (POA)
    /// </summary>

    /// <param name="filter"></param>
    /// <returns></returns>

    [HttpPost("getPermohonanPengisianPOA")]
    public async Task<IActionResult> GetPermohonanPengisianPOA([FromBody] PermohonanPengisianPOAFilterDto filter)
    {
        _logger.LogInformation("Getting Permohonan Pengisian Jawatan (POA)");
        

        var data = await _service.GetPermohonanListPOAAsync(filter);
        return Ok(new
        {
            status = data.Count() > 0 ? "Sucess" : "Failed",
            items = data

        });
    }

    /// <summary>
    ///Create PermohonanPengisianPOA
    /// </summary>
    /// <param name="savePermohonanPengisianPOA"></param>
    /// <returns></returns>

    [HttpPost("newPermohonanPengisianPOA")]
    public async Task<IActionResult> Create([FromBody] SavePermohonanPengisianPOARequestDto savePermohonanPengisianPOA)
    {
        _logger.LogInformation("Creating a new PermohonanPengisianPOA");

       

        try
        {
            var kod = await _service.CreateAsync(savePermohonanPengisianPOA);

            if (string.IsNullOrEmpty(kod))
                return StatusCode(500, "Failed to create the record.");

            return Ok("Created successfully : "+ kod);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during creation : " + ex.InnerException.ToString());
            return StatusCode(500, ex.InnerException.Message.ToString());
        }
    }
    /// <summary>
    ///validate duplicate PermohonanPengisianPOA
    /// </summary>
    /// <param name="savePermohonanPengisianPOA"></param>
    /// <returns></returns>
    [HttpPost("valKumpulanPerkhidmataPOA")]
    public async Task<IActionResult> ValPermohonanPengisianPOA([FromBody] SavePermohonanPengisianPOARequestDto savePermohonanPengisianPOA)
    {
        try
        {
            var isDuplicate = await _service.CheckDuplicateKodNamaAsync(savePermohonanPengisianPOA);
            if (isDuplicate)
                return Conflict("Kod or Nama already exists for another record.");

            return Ok(isDuplicate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during creation");
            return StatusCode(500, ex.InnerException.Message.ToString());
        }
    }

    /// <summary>
    ///Get Permohonan Pengisian Header
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// All filter parameters are optional - if not provided, they will be ignored in the search.
    /// 
    /// </remarks>

    [HttpPost("getPermohonanPengisianByAgensiAndIdPOA")]
    public async Task<IActionResult> GetPermohonanPengisianByAgensiAndId([FromBody] PermohonanPengisianfilterdto filter)
    {
        _logger.LogInformation("Get Permohonan Pengisian ByAgensi AndId ");



        try
        {
            var data = await _service.GetPermohonanPengisianByAgensiAndId(filter);

            if (data == null)
                return NotFound("No matching record found");

            return Ok(new
            {
                status = "Success" ,
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetPermohonanPengisianByAgensiAndId: Error occurred in controller while processing request with filter: {@Filter}", filter);

            return StatusCode(500, new
            {
                status = "Error",
                items = new PermohonanPengisianHeaderResponseDto()
            });
        }
    }


    /// <summary>
    ///Get BilanganPermohonan Pengisian Table POA 
    /// <param name="idPermohonanPengisian">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// All filter parameters are optional - if not provided, they will be ignored in the search.
    /// 
    /// </remarks>

    [HttpGet("getBilanganPermohonanPengisianIdPOA/{idPermohonanPengisian}")]
    public async Task<IActionResult> GetBilanganPermohonanPengisianIdPOA(int idPermohonanPengisian)
    {
        _logger.LogInformation("Get BilanganPermohonan Pengisia Id ");



        try
        {
            var result = await _service.GetBilanganPermohonanPengisianId(idPermohonanPengisian);

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result

            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetBilanganPermohonanPengisianIdPOA: Error occurred in controller while processing request with ID: {@idPermohonanPengisian}", idPermohonanPengisian);

            return StatusCode(500, new
            {
                status = "Error",
                items = new PermohonanPengisianHeaderResponseDto()
            });
        }
    }

    /// <summary>
    ///Update Permohonan Pengisian POA
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("setPermohonanPengisianPOA")]
    public async Task<IActionResult> Update([FromBody] SavePermohonanPengisianPOARequestDto dto)
    {
        _logger.LogInformation("update Permohonan Pengisian POA");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var isSuccess = await _service.UpdateAsync(dto);

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
    /// Get Permohonan Pengisian POAI
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("getPermohonanPengisianPOAI")]
    public async Task<IActionResult> GetPermohonanPengisianPOAI([FromBody] PermohonanPengisianPOAIFilterDto filter)
    {
        _logger.LogInformation("Getting Permohonan Pengisian Jawatan (POAI)");


        var data = await _service.GetPermohonanListPOAIAsync(filter);
        return Ok(new
        {
            status = data.Count() > 0 ? "Sucess" : "Failed",
            items = data

        });
    }

    /// <summary>
    /// Get Filtered Papar Permohonan Pengisian  Jawatan 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("getFilteredPermohonanPengisiaJawatan")]
    public async Task<IActionResult> GetFilteredPermohonanPengisiaJawatanAsync([FromBody] PermohonanPengisianJawatanFilterDto filter)
    {
        _logger.LogInformation("Getting  Papar Permohonan Pengisian Jawatan");


        var data = await _service.GetFilteredPermohonanJawatanAsync(filter);
        return Ok(new
        {
            status = data.Count() > 0 ? "Sucess" : "Failed",
            items = data

        });
    }

    /// <summary>
    //Get Jawatan By Skim And Agensi
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

    [HttpPost("getJawatanBySkimAndAgensi")]
    public async Task<IActionResult> GetJawatanBySkimAndAgensi([FromBody] PenolongPegawaiTeknologiMaklumatFilterDto filter )
    {
        _logger.LogInformation("Get Jawatan By Skim And Agensi");



        try
        {
            var result = await _service.GetJawatanBySkimAndAgensiAsync(filter);

            return Ok(new
            {
                status = result.IdSkimPerkhidmatan > 0 ? "Sucess" : "Failed",
                items = result

            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetJawatanBySkimAndAgensi: Error occurred in controller while processing request");

            return StatusCode(500, new
            {
                status = "Error",
                items = new List<SkimNameWithJawatanDto>()
            });
        }
    }
    /// <summary>
    //Get Grouped Jawatan By Agensi
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

    [HttpPost("getGroupedJawatanByAgensi")]
    public async Task<IActionResult> GetGroupedJawatanByAgensi([FromBody] PenolongPegawaiTeknologiMaklumatFilterDto filter)
    {
        _logger.LogInformation("Get Grouped Jawatan By Agensi");



        try
        {
            var result = await _service.GetGroupedJawatanByAgensiAsync(filter);

            return Ok(new
            {
                status = result.Count > 0 ? "Sucess" : "Failed",
                items = result

            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetGroupedJawatanByAgensi: Error occurred in controller while processing request");

            return StatusCode(500, new
            {
                status = "Error",
                items = new List<SkimNameWithJawatanDto>()
            });
        }
    }
    /// <summary>
    /// Get Simulasi Kewangan by Agensi
    /// </summary>
    /// <param name="agensiId"></param>
    /// <returns></returns>
   // [HttpGet("getSimulasiByAgensi/{agensiId}")]
    //public async Task<IActionResult> GetSimulasiByAgensi(int agensiId)
    //{
    //    var result = await _service.GetSimulasiByAgensiAsync(agensiId);
    //    return Ok(result);
    //}


}
