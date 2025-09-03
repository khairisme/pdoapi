using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/butiran-permohonan-skim-gred-kuj")]
    public class ButiranPermohonanSkimGredKUJExtController : ControllerBase
    {
        private readonly ILogger<ButiranPermohonanSkimGredKUJExtController> _logger;
        private readonly IButiranPermohonanSkimGredKUJExt _butiranpermohonanskimgredkujext;

        public ButiranPermohonanSkimGredKUJExtController(IButiranPermohonanSkimGredKUJExt butiranpermohonanskimgredkujext, ILogger<ButiranPermohonanSkimGredKUJExtController> logger)
        {
            _butiranpermohonanskimgredkujext = butiranpermohonanskimgredkujext;
            _logger = logger;
        }

        [HttpPost("tambah")]
        public async Task<ActionResult> TambahButiranPermohonanSkimGredKUJ([FromQuery] Guid UserId, TambahButiranPermohonanSkimGredKUJDto request)
        {
            _logger.LogInformation("Calling TambahButiranPermohonanSkimGredKUJ");
            try
            {
                await _butiranpermohonanskimgredkujext.TambahButiranPermohonanSkimGredKUJ(UserId,request);
                return CreatedAtAction(nameof(TambahButiranPermohonanSkimGredKUJ), new {UserId, request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in TambahButiranPermohonanSkimGredKUJ");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("senarai")]
        public async Task<ActionResult<IEnumerable<ButiranPermohonanSkimGredKUJDto>>>SenaraiButiranPermohonanSkimGredKUJ()
        {
            _logger.LogInformation("Calling SenaraiButiranPermohonanSkimGredKUJ");
            try
            {
                var data = await _butiranpermohonanskimgredkujext.SenaraiButiranPermohonanSkimGredKUJ();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiButiranPermohonanSkimGredKUJ");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("baca/{id}")]
        public async Task<ActionResult<ButiranPermohonanSkimGredKUJDto>> BacaButiranPermohonanSkimGredKUJ(int Id)
        {
            _logger.LogInformation("Calling BacaButiranPermohonanSkimGredKUJ");
            try
            {
                var data = await _butiranpermohonanskimgredkujext.BacaButiranPermohonanSkimGredKUJ(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in BacaButiranPermohonanSkimGredKUJ");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpDelete("hapus-terus/{id}")]
        public async Task<ActionResult> HapusTerusButiranPermohonanSkimGredKUJ([FromQuery] Guid UserId, int Id)
        {
            _logger.LogInformation("Calling HapusTerusButiranPermohonanSkimGredKUJ");
            try
            {
                await _butiranpermohonanskimgredkujext.HapusTerusButiranPermohonanSkimGredKUJ(UserId,Id);
                return CreatedAtAction(nameof(HapusTerusButiranPermohonanSkimGredKUJ), new {UserId, Id }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in HapusTerusButiranPermohonanSkimGredKUJ");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> KemaskiniButiranPermohonanSkimGredKUJ([FromQuery] Guid UserId, [FromQuery] int Id, [FromBody] ButiranPermohonanSkimGredKUJDto request)
        {
            _logger.LogInformation("Calling KemaskiniButiranPermohonanSkimGredKUJ");
            try
            {
                await _butiranpermohonanskimgredkujext.KemaskiniButiranPermohonanSkimGredKUJ(UserId,Id,request);
                return CreatedAtAction(nameof(KemaskiniButiranPermohonanSkimGredKUJ), new {UserId, Id,request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in KemaskiniButiranPermohonanSkimGredKUJ");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
