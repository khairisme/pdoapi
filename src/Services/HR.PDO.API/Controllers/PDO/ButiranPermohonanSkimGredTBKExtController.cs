using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/butiran-permohonan-skim-gred-tbk")]
    public class ButiranPermohonanSkimGredTBKExtController : ControllerBase
    {
        private readonly ILogger<ButiranPermohonanSkimGredTBKExtController> _logger;
        private readonly IButiranPermohonanSkimGredTBKExt _butiranpermohonanskimgredtbkext;

        public ButiranPermohonanSkimGredTBKExtController(IButiranPermohonanSkimGredTBKExt butiranpermohonanskimgredtbkext, ILogger<ButiranPermohonanSkimGredTBKExtController> logger)
        {
            _butiranpermohonanskimgredtbkext = butiranpermohonanskimgredtbkext;
            _logger = logger;
        }

        [HttpPost("tambah")]
        public async Task<ActionResult> TambahButiranPermohonanSkimGredTBK([FromQuery] Guid UserId, TambahButiranPermohonanSkimGredTBKDto request)
        {
            _logger.LogInformation("Calling TambahButiranPermohonanSkimGredTBK");
            try
            {
                await _butiranpermohonanskimgredtbkext.TambahButiranPermohonanSkimGredTBK(UserId,request);
                return CreatedAtAction(nameof(TambahButiranPermohonanSkimGredTBK), new {UserId, request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in TambahButiranPermohonanSkimGredTBK");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("senarai")]
        public async Task<ActionResult<IEnumerable<ButiranPermohonanSkimGredTBKDto>>>SenaraiButiranPermohonanSkimGredTBK()
        {
            _logger.LogInformation("Calling SenaraiButiranPermohonanSkimGredTBK");
            try
            {
                var data = await _butiranpermohonanskimgredtbkext.SenaraiButiranPermohonanSkimGredTBK();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiButiranPermohonanSkimGredTBK");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("baca/{id}")]
        public async Task<ActionResult<ButiranPermohonanSkimGredTBKDto>> BacaButiranPermohonanSkimGredTBK(int Id)
        {
            _logger.LogInformation("Calling BacaButiranPermohonanSkimGredTBK");
            try
            {
                var data = await _butiranpermohonanskimgredtbkext.BacaButiranPermohonanSkimGredTBK(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in BacaButiranPermohonanSkimGredTBK");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpDelete("hapus-terus/{id}")]
        public async Task<ActionResult> HapusTerusButiranPermohonanSkimGredTBK([FromQuery] Guid UserId, int Id)
        {
            _logger.LogInformation("Calling HapusTerusButiranPermohonanSkimGredTBK");
            try
            {
                await _butiranpermohonanskimgredtbkext.HapusTerusButiranPermohonanSkimGredTBK(UserId,Id);
                return CreatedAtAction(nameof(HapusTerusButiranPermohonanSkimGredTBK), new {UserId, Id }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in HapusTerusButiranPermohonanSkimGredTBK");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> KemaskiniButiranPermohonanSkimGredTBK([FromQuery] Guid UserId, [FromQuery] int Id, [FromBody] ButiranPermohonanSkimGredTBKDto request)
        {
            _logger.LogInformation("Calling KemaskiniButiranPermohonanSkimGredTBK");
            try
            {
                await _butiranpermohonanskimgredtbkext.KemaskiniButiranPermohonanSkimGredTBK(UserId,Id,request);
                return CreatedAtAction(nameof(KemaskiniButiranPermohonanSkimGredTBK), new {UserId, Id,request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in KemaskiniButiranPermohonanSkimGredTBK");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
