using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/unit-organisasi")]
    public class UnitOrganisasiExtController : ControllerBase
    {
        private readonly ILogger<UnitOrganisasiExtController> _logger;
        private readonly IUnitOrganisasiExt _unitorganisasiext;

        public UnitOrganisasiExtController(IUnitOrganisasiExt unitorganisasiext, ILogger<UnitOrganisasiExtController> logger)
        {
            _unitorganisasiext = unitorganisasiext;
            _logger = logger;
        }

        [HttpGet("struktur")]
        public async Task<ActionResult<IEnumerable<StrukturUnitOrganisasiDto>>>StrukturUnitOrganisasi(string? KodCartaOrganisasi)
        {
            _logger.LogInformation("Calling StrukturUnitOrganisasi");
            try
            {
                var data = await _unitorganisasiext.StrukturUnitOrganisasi(KodCartaOrganisasi);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in StrukturUnitOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> KemaskiniUnitOrganisasi([FromQuery] Guid UserId, [FromQuery] int Id, [FromBody] UnitOrganisasiDaftarDto request)
        {
            _logger.LogInformation("Calling KemaskiniUnitOrganisasi");
            try
            {
                await _unitorganisasiext.KemaskiniUnitOrganisasi(UserId,Id,request);
                return CreatedAtAction(nameof(KemaskiniUnitOrganisasi), new {UserId, Id,request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in KemaskiniUnitOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpDelete("hapus-terus/{id}")]
        public async Task<ActionResult> HapusTerusUnitOrganisasi([FromQuery] Guid UserId, int Id)
        {
            _logger.LogInformation("Calling HapusTerusUnitOrganisasi");
            try
            {
                await _unitorganisasiext.HapusTerusUnitOrganisasi(UserId,Id);
                return CreatedAtAction(nameof(HapusTerusUnitOrganisasi), new {UserId, Id }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in HapusTerusUnitOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("Carian")]
        public async Task<ActionResult<IEnumerable<UnitOrganisasiDto>>>CarianUnitOrganisasi(UnitOrganisasiCarianDto request)
        {
            _logger.LogInformation("Calling CarianUnitOrganisasi");
            try
            {
                var data = await _unitorganisasiext.CarianUnitOrganisasi(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in CarianUnitOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanUnitOrganisasi()
        {
            _logger.LogInformation("Calling RujukanUnitOrganisasi");
            try
            {
                var data = await _unitorganisasiext.RujukanUnitOrganisasi();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanUnitOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitOrganisasiDto>>>SenaraiUnitOrganisasi(UnitOrganisasiCarianDto request)
        {
            _logger.LogInformation("Calling SenaraiUnitOrganisasi");
            try
            {
                var data = await _unitorganisasiext.SenaraiUnitOrganisasi(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiUnitOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPatch("penjenamaan/{id}")]
        public async Task<ActionResult> PenjenamaanSemulaUnitOrganisasi([FromQuery] Guid UserId, [FromQuery] int Id, [FromQuery] string? Nama)
        {
            _logger.LogInformation("Calling PenjenamaanSemulaUnitOrganisasi");
            try
            {
                await _unitorganisasiext.PenjenamaanSemulaUnitOrganisasi(UserId,Id,Nama);
                return CreatedAtAction(nameof(PenjenamaanSemulaUnitOrganisasi), new {UserId, Id,Nama }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in PenjenamaanSemulaUnitOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
