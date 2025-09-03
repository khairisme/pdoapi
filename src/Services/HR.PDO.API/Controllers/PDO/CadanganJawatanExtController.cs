using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/cadangan-jawatan")]
    public class CadanganJawatanExtController : ControllerBase
    {
        private readonly ILogger<CadanganJawatanExtController> _logger;
        private readonly ICadanganJawatanExt _cadanganjawatanext;

        public CadanganJawatanExtController(ICadanganJawatanExt cadanganjawatanext, ILogger<CadanganJawatanExtController> logger)
        {
            _cadanganjawatanext = cadanganjawatanext;
            _logger = logger;
        }

        [HttpGet("senarai")]
        public async Task<ActionResult<IEnumerable<CadanganJawatanDto>>>SenaraiCadanganJawatan(int IdPermohonanJawatan)
        {
            _logger.LogInformation("Calling SenaraiCadanganJawatan");
            try
            {
                var data = await _cadanganjawatanext.SenaraiCadanganJawatan(IdPermohonanJawatan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiCadanganJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPost("tambah")]
        public async Task<ActionResult> TambahCadanganJawatan([FromQuery] Guid UserId, CadanganJawatanRequestDto request)
        {
            _logger.LogInformation("Calling TambahCadanganJawatan");
            try
            {
                await _cadanganjawatanext.TambahCadanganJawatan(UserId,request);
                return CreatedAtAction(nameof(TambahCadanganJawatan), new {UserId, request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in TambahCadanganJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPatch("unit-organisasi")]
        public async Task<ActionResult> KemaskiniCadanganJawatan([FromQuery] Guid UserId, int IdButiranPermohonan, int IdUnitOrganisasi)
        {
            _logger.LogInformation("Calling TambahCadanganJawatan");
            try
            {
                await _cadanganjawatanext.KemaskiniCadanganJawatan(UserId, IdButiranPermohonan, IdUnitOrganisasi);
                return CreatedAtAction(nameof(TambahCadanganJawatan), new { UserId, IdButiranPermohonan, IdUnitOrganisasi }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in TambahCadanganJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }
    }
}
