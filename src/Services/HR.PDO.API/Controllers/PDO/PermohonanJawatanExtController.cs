using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/permohonan-jawatan")]
    public class PermohonanJawatanExtController : ControllerBase
    {
        private readonly ILogger<PermohonanJawatanExtController> _logger;
        private readonly IPermohonanJawatanExt _permohonanjawatanext;

        public PermohonanJawatanExtController(IPermohonanJawatanExt permohonanjawatanext, ILogger<PermohonanJawatanExtController> logger)
        {
            _permohonanjawatanext = permohonanjawatanext;
            _logger = logger;
        }

        [HttpGet("carian-norujukan-jenis-tajuk-status")]
        public async Task<ActionResult<IEnumerable<SenaraiPermohonanJawatanDto>>>SenaraiPermohonanJawatanCarianNoRujukanJenisTajukStatus(string? NoRujukan, string? JenisPermohonan, string? TajukPermohonan, string? KodRujStatusPermohonan)
        {
            _logger.LogInformation("Calling SenaraiPermohonanJawatanCarianNoRujukanJenisTajukStatus");
            try
            {
                var data = await _permohonanjawatanext.SenaraiPermohonanJawatanCarianNoRujukanJenisTajukStatus(NoRujukan,JenisPermohonan,TajukPermohonan,KodRujStatusPermohonan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiPermohonanJawatanCarianNoRujukanJenisTajukStatus");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("baca-permohonan-jawatan/{id}")]
        public async Task<ActionResult<BacaPermohonanJawatanDto>> BacaPermohonanJawatan(int Id)
        {
            _logger.LogInformation("Calling BacaPermohonanJawatan");
            try
            {
                var data = await _permohonanjawatanext.BacaPermohonanJawatan(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in BacaPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPost("tambah")]
        public async Task<ActionResult> TambahPermohonanJawatanBaru([FromQuery] Guid UserId, string? NomborRujukan, string? Tajuk, string? Keterangan, string? KodRujJenisPermohonan)
        {
            _logger.LogInformation("Calling TambahPermohonanJawatanBaru");
            try
            {
                await _permohonanjawatanext.TambahPermohonanJawatanBaru(UserId,NomborRujukan,Tajuk,Keterangan,KodRujJenisPermohonan);
                return CreatedAtAction(nameof(TambahPermohonanJawatanBaru), new {UserId, NomborRujukan,Tajuk,Keterangan,KodRujJenisPermohonan }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in TambahPermohonanJawatanBaru");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPut("kemaskini-permohonan-jawatan/{id}")]
        public async Task<ActionResult> KemaskiniPermohonanJawatan([FromQuery] Guid UserId, int Id, PermohonanJawatanDaftarDto filter)
        {
            _logger.LogInformation("Calling KemaskiniPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.KemaskiniPermohonanJawatan(UserId,Id,filter);
                return CreatedAtAction(nameof(KemaskiniPermohonanJawatan), new {UserId, Id,filter }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in KemaskiniPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpDelete("hapus-terus/{id}")]
        public async Task<ActionResult> HapusTerusPermohonanJawatan([FromQuery] Guid UserId, int Id)
        {
            _logger.LogInformation("Calling HapusTerusPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.HapusTerusPermohonanJawatan(UserId,Id);
                return CreatedAtAction(nameof(HapusTerusPermohonanJawatan), new {UserId, Id }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in HapusTerusPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("carian-agensi-norujukan-tajuk-status")]
        public async Task<ActionResult<IEnumerable<SenaraiPermohonanJawatanDto>>>SenaraiPermohonanJawatanCarianAgensiNoRujukanTajukStatus(string? KodUnitOrganisasi, string? NoRujukan, string? TajukPermohonan, string? KodRujStatusPermohonan)
        {
            _logger.LogInformation("Calling SenaraiPermohonanJawatanCarianAgensiNoRujukanTajukStatus");
            try
            {
                var data = await _permohonanjawatanext.SenaraiPermohonanJawatanCarianAgensiNoRujukanTajukStatus(KodUnitOrganisasi,NoRujukan,TajukPermohonan,KodRujStatusPermohonan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiPermohonanJawatanCarianAgensiNoRujukanTajukStatus");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("Carian/{id:int}")]
        [SwaggerOperation(
            Summary = "Carian Permohonan Jawatan mengikut",
            Description = ""
        )]
        public async Task<ActionResult<IEnumerable<SenaraiPermohonanJawatanDto>>>CarianPermohonanJawatan([FromRoute] int Id, [FromBody] PermohonanJawatanCarianDto request)
        {
            _logger.LogInformation("Calling CarianPermohonanJawatan");
            try
            {
                var data = await _permohonanjawatanext.CarianPermohonanJawatan(Id,request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in CarianPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SenaraiPermohonanJawatanDto>>>SenaraiPermohonanJawatan(PermohonanJawatanCarianDto request)
        {
            _logger.LogInformation("Calling SenaraiPermohonanJawatan");
            try
            {
                var data = await _permohonanjawatanext.SenaraiPermohonanJawatan(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult> DaftarPermohonanJawatan([FromQuery] Guid UserId, [FromBody] PermohonanJawatanDaftarDto request)
        {
            _logger.LogInformation("Calling DaftarPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.DaftarPermohonanJawatan(UserId,request);
                return CreatedAtAction(nameof(DaftarPermohonanJawatan), new {UserId, request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in DaftarPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
