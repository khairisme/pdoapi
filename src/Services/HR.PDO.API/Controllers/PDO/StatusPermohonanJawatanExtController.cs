using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/status-permohonan-jawatan")]
    public class StatusPermohonanJawatanExtController : ControllerBase
    {
        private readonly ILogger<StatusPermohonanJawatanExtController> _logger;
        private readonly IStatusPermohonanJawatanExt _statuspermohonanjawatanext;

        public StatusPermohonanJawatanExtController(IStatusPermohonanJawatanExt statuspermohonanjawatanext, ILogger<StatusPermohonanJawatanExtController> logger)
        {
            _statuspermohonanjawatanext = statuspermohonanjawatanext;
            _logger = logger;
        }

        [HttpPost("tambah")]
        public async Task<ActionResult> TambahStatusPermohonanJawatan([FromQuery] Guid UserId, TambahStatusPermohonanJawatanDto request)
        {
            _logger.LogInformation("Calling TambahStatusPermohonanJawatan");
            try
            {
                await _statuspermohonanjawatanext.TambahStatusPermohonanJawatan(UserId,request);
                return CreatedAtAction(nameof(TambahStatusPermohonanJawatan), new {UserId, request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in TambahStatusPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPost("tambah-draft")]
        public async Task<ActionResult> TambahStatusPermohonanJawatanDraft([FromQuery] Guid UserId, TambahStatusPermohonanJawatanDraftDto request)
        {
            _logger.LogInformation("Calling TambahStatusPermohonanJawatanDraft");
            try
            {
                await _statuspermohonanjawatanext.TambahStatusPermohonanJawatanDraft(UserId,request);
                return CreatedAtAction(nameof(TambahStatusPermohonanJawatanDraft), new {UserId, request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in TambahStatusPermohonanJawatanDraft");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPost("tambah-baharu")]
        public async Task<ActionResult> TambahStatusPermohonanJawatanBaharu([FromQuery] Guid UserId, TambahStatusPermohonanJawatanBaharuDto request)
        {
            _logger.LogInformation("Calling TambahStatusPermohonanJawatanBaharu");
            try
            {
                await _statuspermohonanjawatanext.TambahStatusPermohonanJawatanBaharu(UserId,request);
                return CreatedAtAction(nameof(TambahStatusPermohonanJawatanBaharu), new {UserId, request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in TambahStatusPermohonanJawatanBaharu");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
