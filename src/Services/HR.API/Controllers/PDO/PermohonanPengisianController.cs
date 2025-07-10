using HR.Application.DTOs;
using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Application.Services;
using HR.Application.Services.PDO;
using HR.Core.Entities;
using HR.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared.Contracts.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.API.Controllers.PDO;
//[Authorize]
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
                status = result.Count() > 0 ? "Sucess" : "Failed",
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
    /// <param name="idPermohonan"></param>
    /// <returns></returns>
    [HttpGet("GetSimulasiByPermohonanId/{idPermohonan}")]
    public async Task<IActionResult> GetSimulasiByPermohonanId(int idPermohonan)
    {
        try
        {
            var result = await _service.GetSimulasiByPermohonanIdAsync(idPermohonan);
            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result

            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSimulasiByPermohonanId: Error occurred in controller while processing request");

            return StatusCode(500, new
            {
                status = "Error",
                items = new List<SkimNameWithJawatanDto>()
            });
        }
    }
    // By Amar
    /// <summary>
    /// GetSenaraiJawatanSebenar
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// All  parameters are mandatory
    /// 
    /// </remarks>
    [HttpPost("getSenaraiJawatanSebenar")]
    public async Task<IActionResult> GetSenaraiJawatanSebenar([FromBody] SenaraiJawatanSebenarFilterDto filter)
    {

        _logger.LogInformation("GetSenaraiJawatanSebenar: GetSenaraiJawatanSebenar method called from controller with filter: {@Filter}", filter);
        try
        {
            var data = await _service.GetSenaraiJawatanSebenar(filter);

            _logger.LogInformation("GetSenaraiJawatanSebenar: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Success" : "Failed",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSenaraiJawatanSebenar: Error occurred in controller while processing request with filter: {@Filter}", filter);

            return StatusCode(500, new
            {
                status = "Error",
                message = "An error occurred while retrieving application data",
                items = new List<SenaraiJawatanSebenarResponseDto>()
            });
        }



    }

    // By Amar
    /// <summary>
    /// GetImplikasiKewangan
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// All  parameters are mandatory
    /// 
    /// </remarks>
    [HttpPost("getImplikasiKewangan")]
    public async Task<IActionResult> GetImplikasiKewangan([FromBody] ImplikasiKewanganFilterDto filter)
    {

        _logger.LogInformation("GetImplikasiKewangan: GetImplikasiKewangan method called from controller with filter: {@Filter}", filter);
        try
        {
            var data = await _service.GetImplikasiKewangan(filter);

            _logger.LogInformation("GetImplikasiKewangan: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Success" : "Failed",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetImplikasiKewangan: Error occurred in controller while processing request with filter: {@Filter}", filter);

            return StatusCode(500, new
            {
                status = "Error",
                message = "An error occurred while retrieving application data",
                items = new List<ImplikasiKewanganResponseDto>()
            });
        }



    }

    //Amar
    /// <summary>
    /// GetSenaraiPermohonanPengisianJawatan
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// 
    /// Filters are optional. 
    /// 
    /// 
    /// </remarks>
    [HttpPost("getSenaraiPermohonanPengisianJawatan")]

    public async Task<IActionResult> GetSenaraiPermohonanPengisianJawatan([FromBody] SenaraiPermohonanPengisianJawatanFilterDto filter)
    {
        _logger.LogInformation("GetSenaraiPermohonanPengisianJawatan: GetSenaraiPermohonanPengisianJawatan method called from controller with filter: {@Filter}", filter);
        try
        {
            var data = await _service.GetSenaraiPermohonanPengisianJawatan(filter);

            _logger.LogInformation("GetSenaraiPermohonanPengisianJawatan: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Success" : "Failed",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSenaraiPermohonanPengisianJawatan: Error occurred in controller while processing request with filter: {@Filter}", filter);

            return StatusCode(500, new
            {
                status = "Error",
                message = "An error occurred while retrieving application data",
                items = new List<SenaraiPermohonanPengisianJawatanResponseDto>()
            });
        }
    }
    //Amar
    /// <summary>
    /// GetBilanganPermohonanPengisian
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// 
    /// Filters are optional. 
    /// 
    /// 
    /// </remarks>

    [HttpPost("getBilanganPermohonanPengisian")]
    public async Task<IActionResult> GetBilanganPermohonanPengisian([FromBody] BilanganPermohonanPengisianFilterDto filter)
    {
        _logger.LogInformation("GetBilanganPermohonanPengisian: GetBilanganPermohonanPengisian method called from controller with filter: {@Filter}", filter);
        try
        {
            var data = await _service.GetBilanganPermohonanPengisian(filter);

            _logger.LogInformation("GetBilanganPermohonanPengisian: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Success" : "Failed",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetBilanganPermohonanPengisian: Error occurred in controller while processing request with filter: {@Filter}", filter);

            return StatusCode(500, new
            {
                status = "Error",
                message = "An error occurred while retrieving application data..",
                items = new List<BilanganPermohonanPengisianMaklumatPermohonanResponseDto>()
            });
        }
    }

    //Amar
    /// <summary>
    /// SetHantarBilanganPermohonanPengisian
    /// </summary>
    /// <param name="request">List of records to update</param>
    /// <returns>Returns a list of data</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Invalid request data provided</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpPut("setHantarBilanganPermohonanPengisian")]
    public async Task<IActionResult> SetHantarBilanganPermohonanPengisian([FromBody] HantarBilanganPermohonanPengisianRequestDto request)
    {
        _logger.LogInformation("SetHantarBilanganPermohonanPengisian: SetHantarBilanganPermohonanPengisian method called from controller with {Count} items", request?.Items?.Count ?? 0);
        try
        {
            // Validate request
            if (request == null || request.Items == null || !request.Items.Any())
            {
                _logger.LogWarning("SetHantarBilanganPermohonanPengisian: Invalid request - no items provided");
                return BadRequest(new
                {
                    status = "Error",
                    message = "Request must contain at least one item to update"
                });
            }

            // Validate each item has required fields
            var invalidItems = request.Items.Where(x => x.Id <= 0).ToList();
            if (invalidItems.Any())
            {
                _logger.LogWarning("SetHantarBilanganPermohonanPengisian: Invalid request - items with invalid IDs found");
                return BadRequest(new
                {
                    status = "Error",
                    message = "All items must have valid IDs (greater than 0)"
                });
            }

            var updateResult = await _service.SetHantarBilanganPermohonanPengisian(request);

            _logger.LogInformation("SetHantarBilanganPermohonanPengisian: Update operation completed successfully with result: {Result}", updateResult);

            return Ok(new
            {
                status = updateResult ? "Success" : "No Data Updated",
                message = updateResult ? "All records updated successfully" : "No records were updated"
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "SetHantarBilanganPermohonanPengisian: Business logic error occurred in controller");

            return BadRequest(new
            {
                status = "Error",
                message = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SetHantarBilanganPermohonanPengisian: Unexpected error occurred in controller while processing update request");

            return StatusCode(500, new
            {
                status = "Error",
                message = "An error occurred while updating the records. All changes have been rolled back"
            });
        }
    }

    //Amar
    /// <summary>
    /// GetSenaraiJawatanSebenarGroupedAgency
    /// </summary>
    /// 
    /// <returns>Returns a list of data</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// 
    /// 
    /// 
    /// </remarks>
    [HttpGet("getSenaraiJawatanSebenarGroupedAgency")]
    public async Task<IActionResult> GetSenaraiJawatanSebenarGroupedAgency()
    {
        _logger.LogInformation("GetSenaraiJawatanSebenarGroupedAgency: GetSenaraiJawatanSebenarGroupedAgency method called from controller ");
        try
        {
            var data = await _service.GetSenaraiJawatanSebenarGroupedAgency();
            _logger.LogInformation("GetSenaraiJawatanSebenarGroupedAgency: Successfully retrieved {Count} agency records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Success" : "Failed",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSenaraiJawatanSebenarGroupedAgency: Error occurred in controller while processing request");
            return StatusCode(500, new
            {
                status = "Error",
                message = "An error occurred while retrieving jawatan sebenar grouped by agency data",
                items = new List<SenaraiJawatanSebenarGroupedAgencyResponseDto>()
            });
        }
    
}

    // By Amar
    /// <summary>
    /// GetImplikasiKewanganJanaSimulasiKewangan
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// All  parameters are mandatory
    /// 
    /// </remarks>
    [HttpPost("getImplikasiKewanganJanaSimulasiKewangan")]
    public async Task<IActionResult> GetImplikasiKewanganJanaSimulasiKewangan([FromBody] ImplikasiKewanganJanaSimulasiKewanganFilterDto filter)
    {

        _logger.LogInformation("GetImplikasiKewanganJanaSimulasiKewangan: GetImplikasiKewanganJanaSimulasiKewangan method called from controller with filter: {@Filter}", filter);
        try
        {
            var data = await _service.GetImplikasiKewanganJanaSimulasiKewangan(filter);

            _logger.LogInformation("GetImplikasiKewanganJanaSimulasiKewangan: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Success" : "Failed",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetImplikasiKewanganJanaSimulasiKewangan: Error occurred in controller while processing request with filter: {@Filter}", filter);

            return StatusCode(500, new
            {
                status = "Error",
                message = "An error occurred while retrieving application data",
                items = new List<ImplikasiKewanganJanaSimulasiKewanganResponseDto>()
            });
        }



    }
}
