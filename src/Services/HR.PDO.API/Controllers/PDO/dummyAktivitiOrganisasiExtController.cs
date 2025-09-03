using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/dummy/AktivitiOrganisasi")]
    public class dummyAktivitiOrganisasiExtController : ControllerBase
    {
        private readonly ILogger<dummyAktivitiOrganisasiExtController> _logger;
        private readonly IdummyAktivitiOrganisasiExt _dummyaktivitiorganisasiext;

        public dummyAktivitiOrganisasiExtController(IdummyAktivitiOrganisasiExt dummyaktivitiorganisasiext, ILogger<dummyAktivitiOrganisasiExtController> logger)
        {
            _dummyaktivitiorganisasiext = dummyaktivitiorganisasiext;
            _logger = logger;
        }

        [HttpGet("baca/{id}")]
        public async Task<ActionResult<AktivitiOrganisasiDto>> BacaAktivitiOrganisasi(int Id)
        {
            _logger.LogInformation("Calling BacaAktivitiOrganisasi");
            try
            {
                var data = await _dummyaktivitiorganisasiext.BacaAktivitiOrganisasi(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in BacaAktivitiOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPost("wujud")]
        public async Task<ActionResult> WujudUnitOrganisasiBaru([FromQuery] Guid UserId, UnitOrganisasiWujudDto request)
        {
            _logger.LogInformation("Calling WujudUnitOrganisasiBaru");
            try
            {
                await _dummyaktivitiorganisasiext.WujudUnitOrganisasiBaru(UserId,request);
                return CreatedAtAction(nameof(WujudUnitOrganisasiBaru), new {UserId, request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in WujudUnitOrganisasiBaru");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
