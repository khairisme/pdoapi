using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/butiran-permohonan-jawatan")]
    public class ButiranPermohonanJawatanExtController : ControllerBase
    {
        private readonly ILogger<ButiranPermohonanJawatanExtController> _logger;
        private readonly IButiranPermohonanJawatanExt _butiranpermohonanjawatanext;

        public ButiranPermohonanJawatanExtController(IButiranPermohonanJawatanExt butiranpermohonanjawatanext, ILogger<ButiranPermohonanJawatanExtController> logger)
        {
            _butiranpermohonanjawatanext = butiranpermohonanjawatanext;
            _logger = logger;
        }

        [HttpPost("tambah")]
        public async Task<ActionResult> TambahButiranPermohonanJawatan(TambahButiranPermohonanJawatanDto request)
        {
            _logger.LogInformation("Calling TambahButiranPermohonanJawatan");
            try
            {
                await _butiranpermohonanjawatanext.TambahButiranPermohonanJawatan(request);
                return CreatedAtAction(nameof(TambahButiranPermohonanJawatan), new {request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in TambahButiranPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ButiranPermohonanJawatanDto>>>SenaraiButiranPermohonanJawatan()
        {
            _logger.LogInformation("Calling SenaraiButiranPermohonanJawatan");
            try
            {
                var data = await _butiranpermohonanjawatanext.SenaraiButiranPermohonanJawatan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiButiranPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPost("baca")]
        public async Task<ActionResult<ButiranPermohonanJawatanDto>> BacaButiranPermohonanJawatan(AddButiranPermohonanJawatanRequestDto request)
        {
            _logger.LogInformation("Calling BacaButiranPermohonanJawatan");
            try
            {
                var data = await _butiranpermohonanjawatanext.BacaButiranPermohonanJawatan(request);
                if (data == null)
                {
                    return NotFound(new { message = "Tiada rekod" });

                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in BacaButiranPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpDelete("hapus-terus/{id}")]
        public async Task<ActionResult> HapusTerusButiranPermohonanJawatan([FromQuery] AddButiranPermohonanJawatanRequestDto request)
        {
            _logger.LogInformation("Calling HapusTerusButiranPermohonanJawatan");
            try
            {
                await _butiranpermohonanjawatanext.HapusTerusPermohonanJawatan(request);
                return CreatedAtAction(nameof(HapusTerusButiranPermohonanJawatan), new { request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in HapusTerusPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
