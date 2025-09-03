using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/butiran-permohonan-skim-gred")]
    public class ButiranPermohonanSkimGredExtController : ControllerBase
    {
        private readonly ILogger<ButiranPermohonanSkimGredExtController> _logger;
        private readonly IButiranPermohonanSkimGredExt _butiranpermohonanskimgredext;

        public ButiranPermohonanSkimGredExtController(IButiranPermohonanSkimGredExt butiranpermohonanskimgredext, ILogger<ButiranPermohonanSkimGredExtController> logger)
        {
            _butiranpermohonanskimgredext = butiranpermohonanskimgredext;
            _logger = logger;
        }

        [HttpPost("tambah")]
        public async Task<ActionResult> TambahButiranPermohonanSkimGred([FromQuery] Guid UserId, TambahButiranPermohonanSkimGredDto request)
        {
            _logger.LogInformation("Calling TambahButiranPermohonanSkimGred");
            try
            {
                await _butiranpermohonanskimgredext.TambahButiranPermohonanSkimGred(UserId,request);
                return CreatedAtAction(nameof(TambahButiranPermohonanSkimGred), new {UserId, request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in TambahButiranPermohonanSkimGred");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("senarai")]
        public async Task<ActionResult<IEnumerable<ButiranPermohonanSkimGredDto>>>SenaraiButiranPermohonanSkimGred()
        {
            _logger.LogInformation("Calling SenaraiButiranPermohonanSkimGred");
            try
            {
                var data = await _butiranpermohonanskimgredext.SenaraiButiranPermohonanSkimGred();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiButiranPermohonanSkimGred");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("baca/{id}")]
        public async Task<ActionResult<ButiranPermohonanSkimGredDto>> BacaButiranPermohonanSkimGred(int Id)
        {
            _logger.LogInformation("Calling BacaButiranPermohonanSkimGred");
            try
            {
                var data = await _butiranpermohonanskimgredext.BacaButiranPermohonanSkimGred(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in BacaButiranPermohonanSkimGred");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpDelete("hapus-terus/{id}")]
        public async Task<ActionResult> HapusTerusButiranPermohonanSkimGred([FromQuery] Guid UserId, int Id)
        {
            _logger.LogInformation("Calling HapusTerusButiranPermohonanSkimGred");
            try
            {
                await _butiranpermohonanskimgredext.HapusTerusButiranPermohonanSkimGred(UserId,Id);
                return CreatedAtAction(nameof(HapusTerusButiranPermohonanSkimGred), new {UserId, Id }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in HapusTerusButiranPermohonanSkimGred");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> KemaskiniButiranPermohonanSkimGred([FromQuery] Guid UserId, [FromQuery] int Id, [FromBody] ButiranPermohonanSkimGredDto request)
        {
            _logger.LogInformation("Calling KemaskiniButiranPermohonanSkimGred");
            try
            {
                await _butiranpermohonanskimgredext.KemaskiniButiranPermohonanSkimGred(UserId,Id,request);
                return CreatedAtAction(nameof(KemaskiniButiranPermohonanSkimGred), new {UserId, Id,request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in KemaskiniButiranPermohonanSkimGred");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
