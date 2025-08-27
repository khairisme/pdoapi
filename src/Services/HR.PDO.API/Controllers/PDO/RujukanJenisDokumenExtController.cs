using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-jenis-dokumen")]
    public class RujukanJenisDokumenExtController : ControllerBase
    {
        private readonly ILogger<RujukanJenisDokumenExtController> _logger;
        private readonly IRujJenisDokumenExt _rujjenisdokumenext;

        public RujukanJenisDokumenExtController(IRujJenisDokumenExt rujjenisdokumenext, ILogger<RujukanJenisDokumenExtController> logger)
        {
            _rujjenisdokumenext = rujjenisdokumenext;
            _logger = logger;
        }

        [HttpGet("Carian")]
        public async Task<ActionResult<IEnumerable<RujJenisDokumenDto>>>CarianRujJenisDokumen(RujJenisDokumenCarianDto request)
        {
            _logger.LogInformation("Calling CarianRujJenisDokumen");
            try
            {
                var data = await _rujjenisdokumenext.CarianRujJenisDokumen(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in CarianRujJenisDokumen");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RujJenisDokumenDto>>>SenaraiRujJenisDokumen(RujJenisDokumenCarianDto request)
        {
            _logger.LogInformation("Calling SenaraiRujJenisDokumen");
            try
            {
                var data = await _rujjenisdokumenext.SenaraiRujJenisDokumen(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiRujJenisDokumen");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanRujJenisDokumen(RujJenisDokumenDaftarDto request)
        {
            _logger.LogInformation("Calling RujukanRujJenisDokumen");
            try
            {
                var data = await _rujjenisdokumenext.RujukanRujJenisDokumen(request);
                return Ok(data);
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
                await _rujjenisdokumenext.DaftarRujJenisDokumen(UserId,request);
                return CreatedAtAction(nameof(DaftarRujJenisDokumen), new {UserId, request }, null);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> KemaskiniRujJenisDokumen([FromQuery] Guid UserId, int Id, RujJenisDokumenDaftarDto filter)
        {
            _logger.LogInformation("Calling KemaskiniRujJenisDokumen");
            try
            {
                await _rujjenisdokumenext.KemaskiniRujJenisDokumen(UserId,Id,filter);
                return CreatedAtAction(nameof(KemaskiniRujJenisDokumen), new {UserId, Id,filter }, null);
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
