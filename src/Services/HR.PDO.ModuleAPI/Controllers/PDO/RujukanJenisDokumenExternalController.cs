using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/jenis-dokumen")]
    public class RujukanJenisDokumenExternalController : ControllerBase
    {
        private readonly ILogger<RujukanJenisDokumenExternalController> _logger;
        private readonly IRujukanJenisDokumenExt _rujjenisdokumenext;

        public RujukanJenisDokumenExternalController(IRujukanJenisDokumenExt rujjenisdokumenext, ILogger<RujukanJenisDokumenExternalController> logger)
        {
            _rujjenisdokumenext = rujjenisdokumenext;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanRujJenisDokumen()
        {
            _logger.LogInformation("Calling RujukanRujJenisDokumen");
            try
            {
                var data = await _rujjenisdokumenext.RujukanJenisDokumen();
                return Ok(new
                {
                    status = data.Count() > 0 ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanRujJenisDokumen");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult> DaftarRujJenisDokumen([FromQuery] Guid UserId, RujJenisDokumenDaftarDto request)
        {
            _logger.LogInformation("Calling DaftarRujJenisDokumen");
            try
            {
                await _rujjenisdokumenext.DaftarRujJenisDokumen(request);
                return Ok(new { message = "Berjaya Daftar Jenis Dokumen" });
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in DaftarRujJenisDokumen");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPut]
        public async Task<ActionResult> KemaskiniRujJenisDokumen([FromQuery] Guid UserId,RujJenisDokumenDaftarDto filter)
        {
            _logger.LogInformation("Calling KemaskiniRujJenisDokumen");
            try
            {
                await _rujjenisdokumenext.KemaskiniRujJenisDokumen(filter);
                return Ok(new { message = "Berjaya Kemaskini Jenis Dokumen" });
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in KemaskiniRujJenisDokumen");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
