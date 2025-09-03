using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/dokumen-permohonan")]
    public class DokumenPermohonanExtController : ControllerBase
    {
        private readonly ILogger<DokumenPermohonanExtController> _logger;
        private readonly IDokumenPermohonanExt _dokumenpermohonanext;

        public DokumenPermohonanExtController(IDokumenPermohonanExt dokumenpermohonanext, ILogger<DokumenPermohonanExtController> logger)
        {
            _dokumenpermohonanext = dokumenpermohonanext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> WujudDokumenPermohonanBaru([FromQuery] Guid UserId, int IdPermohonanJawatan, string? KodRujJenisDokumen, string? NamaDokumen, string? PautanDokumen, string? FormatDokumen, int Saiz)
        {
            _logger.LogInformation("Calling WujudDokumenPermohonanBaru");
            try
            {
                await _dokumenpermohonanext.WujudDokumenPermohonanBaru(UserId,IdPermohonanJawatan,KodRujJenisDokumen,NamaDokumen,PautanDokumen,FormatDokumen,Saiz);
                return CreatedAtAction(nameof(WujudDokumenPermohonanBaru), new {UserId, IdPermohonanJawatan,KodRujJenisDokumen,NamaDokumen,PautanDokumen,FormatDokumen,Saiz }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in WujudDokumenPermohonanBaru");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> HapusTerusDokumenPermohonan([FromQuery] Guid UserId, int Id)
        {
            _logger.LogInformation("Calling HapusTerusDokumenPermohonan");
            try
            {
                await _dokumenpermohonanext.HapusTerusDokumenPermohonan(UserId,Id);
                return CreatedAtAction(nameof(HapusTerusDokumenPermohonan), new {UserId, Id }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in HapusTerusDokumenPermohonan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RujJenisDokumenLinkDto>>>SenaraiDokumenPermohonan(int IdPermohonanJawatan)
        {
            _logger.LogInformation("Calling SenaraiDokumenPermohonan");
            try
            {
                var data = await _dokumenpermohonanext.SenaraiDokumenPermohonan(IdPermohonanJawatan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiDokumenPermohonan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
