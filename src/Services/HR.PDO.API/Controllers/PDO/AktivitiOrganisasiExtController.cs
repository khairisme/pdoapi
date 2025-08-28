using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/aktiviti-organisasi")]
    public class AktivitiOrganisasiExtController : ControllerBase
    {
        private readonly ILogger<AktivitiOrganisasiExtController> _logger;
        private readonly IAktivitiOrganisasiExt _aktivitiorganisasiext;

        public AktivitiOrganisasiExtController(IAktivitiOrganisasiExt aktivitiorganisasiext, ILogger<AktivitiOrganisasiExtController> logger)
        {
            _aktivitiorganisasiext = aktivitiorganisasiext;
            _logger = logger;
        }

        [HttpGet("baca/{id}")]
        public async Task<ActionResult<AktivitiOrganisasiDto>> BacaAktivitiOrganisasi(int Id)
        {
            _logger.LogInformation("Calling BacaAktivitiOrganisasi");
            try
            {
                var data = await _aktivitiorganisasiext.BacaAktivitiOrganisasi(Id);
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

        [HttpPost("tambah")]
        public async Task<ActionResult> WujudAktivitiOrganisasiBaru([FromQuery] Guid UserId, int IdIndukAktivitiOrganisasi, string? KodProgram, string? Kod, string? Nama, int Tahap, string? KodRujKategoriAktivitiOrganisasi, string? Keterangan)
        {
            _logger.LogInformation("Calling WujudAktivitiOrganisasiBaru");
            try
            {
                await _aktivitiorganisasiext.WujudAktivitiOrganisasiBaru(UserId,IdIndukAktivitiOrganisasi,KodProgram,Kod,Nama,Tahap,KodRujKategoriAktivitiOrganisasi,Keterangan);
                return CreatedAtAction(nameof(WujudAktivitiOrganisasiBaru), new {UserId, IdIndukAktivitiOrganisasi,KodProgram,Kod,Nama,Tahap,KodRujKategoriAktivitiOrganisasi,Keterangan }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in WujudAktivitiOrganisasiBaru");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPut("penjenamaan-semula/{id}")]
        public async Task<ActionResult> PenjenamaanAktivitiOrganisasi([FromQuery] Guid UserId, int Id, string? Nama)
        {
            _logger.LogInformation("Calling PenjenamaanAktivitiOrganisasi");
            try
            {
                await _aktivitiorganisasiext.PenjenamaanAktivitiOrganisasi(UserId,Id,Nama);
                return CreatedAtAction(nameof(PenjenamaanAktivitiOrganisasi), new {UserId, Id,Nama }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in PenjenamaanAktivitiOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpDelete("hapus-terus/{id}")]
        public async Task<ActionResult> HapusTerusAktivitiOrganisasi([FromQuery] Guid UserId, int Id)
        {
            _logger.LogInformation("Calling HapusTerusAktivitiOrganisasi");
            try
            {
                await _aktivitiorganisasiext.HapusTerusAktivitiOrganisasi(UserId,Id);
                return CreatedAtAction(nameof(HapusTerusAktivitiOrganisasi), new {UserId, Id }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in HapusTerusAktivitiOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("struktur")]
        public async Task<ActionResult<IEnumerable<StrukturAktivitiOrganisasiDto>>>StrukturAktivitiOrganisasi(string? KodCartaOrganisasi, int parentId = 0, int page = 1, int pageSize = 50, string? Keyword = null, string? sortBy = "AktivitiOrganisasi", bool desc = false)
        {
            _logger.LogInformation("Calling StrukturAktivitiOrganisasi");
            try
            {
                var data = await _aktivitiorganisasiext.StrukturAktivitiOrganisasi(KodCartaOrganisasi,parentId,page,pageSize,Keyword,sortBy,desc);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in StrukturAktivitiOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
