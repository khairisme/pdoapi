using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/permohonan-jawatan")]
    public class PermohonanJawatanExtController : ControllerBase
    {
        private readonly ILogger<PermohonanJawatanExtController> _logger;
        private readonly IPermohonanJawatanExt _permohonanjawatanext;

        public PermohonanJawatanExtController(IPermohonanJawatanExt permohonanjawatanext, ILogger<PermohonanJawatanExtController> logger)
        {
            _permohonanjawatanext = permohonanjawatanext;
            _logger = logger;
        }

        [HttpGet("baca/{Id}")]
        public async Task<ActionResult<BacaPermohonanJawatanDto>> BacaPermohonanJawatan(int Id)
        {
            _logger.LogInformation("Calling BacaPermohonanJawatan");
            try
            {
                var data = await _permohonanjawatanext.BacaPermohonanJawatan(Id);
                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data

                });
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

        [HttpGet("semak/{Id}")]
        public async Task<ActionResult<SemakPermohonanJawatanDto>> SemakPermohonanJawatan(int Id)
        {
            _logger.LogInformation("Calling BacaPermohonanJawatan");
            try
            {
                var data = await _permohonanjawatanext.SemakPermohonanJawatan(Id);
                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in BacaPermohonanJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
            }
        }

        [HttpGet("muat")]
        public async Task<ActionResult<MuatPermohonanJawatanOutputDto>> MuatPermohonanJawatan(int IdUnitOrganisasi, string? NomborRujukanPrefix)
        {
            _logger.LogInformation("Calling MuatPermohonanJawatan");
            try
            {
                var data = await _permohonanjawatanext.MuatPermohonanJawatan(IdUnitOrganisasi, NomborRujukanPrefix);
                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in MuatPermohonanJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
            }
        }
        [HttpPost("tambah")]
        public async Task<ActionResult> TambahPermohonanJawatanBaru([FromBody] TambahPermohonanJawatanBaruDto request)
        {
            _logger.LogInformation("Calling TambahPermohonanJawatanBaru");
            try
            {
                var result = await _permohonanjawatanext.TambahPermohonanJawatanBaru(request);
                return Ok(result);
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

        [HttpPut("kemaskini-semak")]
        public async Task<ActionResult> KemaskiniSemakPermohonanJawatan([FromBody] SemakPermohonanJawatanRequestDto request)
        {
            _logger.LogInformation("Calling KemaskiniSemakPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.KemaskiniSemakPermohonanJawatan(request);
                return Ok(new { message = "Berjaya Semak Permohonan jawatan" });
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

        [HttpPut("kemaskini-permohonan-jawatan")]
        public async Task<ActionResult> KemaskiniPermohonanJawatan([FromBody] PermohonanJawatanDaftarDto request)
        {
            _logger.LogInformation("Calling KemaskiniPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.KemaskiniPermohonanJawatan(request);
                return Ok(new { message = "Berjaya Kemaskini Permohonan jawatan" });
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in KemaskiniPermohonanJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
            }
        }

        [HttpDelete("hapus-terus")]
        public async Task<ActionResult> HapusTerusPermohonanJawatan([FromQuery] int IdPermohonanJawatan)
        {
            _logger.LogInformation("Calling HapusTerusPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.HapusTerusPermohonanJawatan(IdPermohonanJawatan);
                return CreatedAtAction(nameof(HapusTerusPermohonanJawatan), new { IdPermohonanJawatan }, null);
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

        [HttpPost("carian")]
        [SwaggerOperation(
            Summary = "Carian Permohonan Jawatan by IdUnitOrganisasi/AgensiId/KodRujJenisPermohonan/KodRujStatusPermohonanJawatan/NomborRujukan/TajukPermohonan",
            Description = "Search permohonan jawatan using query filters (keyword, page, pageSize, sortBy, desc).",
            OperationId = "CarianPermohonanJawatan",
            Tags = new[] { "PermohonanJawatanExt" }
        )]
        public async Task<ActionResult<IEnumerable<SenaraiPermohonanJawatanDto>>> CarianPermohonanJawatan([FromBody] PermohonanJawatanCarianDto request)
        {
            _logger.LogInformation("Calling CarianPermohonanJawatan");
            try
            {
                if (request.KodRujStatusPermohonanJawatan == "")
                    request.KodRujStatusPermohonanJawatan = null;
                if (request.TajukPermohonan == "")
                    request.TajukPermohonan = null;
                if (request.NomborRujukan == "")
                    request.NomborRujukan = null;
                if (request.KodRujJenisPermohonan == "")
                    request.KodRujJenisPermohonan = null;
                var data = await _permohonanjawatanext.CarianPermohonanJawatan(request);
                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CarianPermohonanJawatan");
                var err = ex.InnerException?.Message ?? "";
                return StatusCode(500, ex.Message + (string.IsNullOrEmpty(err) ? "" : "-" + err));
            }
        }
        //[SwaggerOperation(
        //    Summary = "Carian Permohonan Jawatan mengikut",
        //    Description = ""
        //)]
        //public async Task<ActionResult<IEnumerable<SenaraiPermohonanJawatanDto>>> CarianPermohonanJawatan([FromBody] PermohonanJawatanCarianDto request)
        //{
        //    _logger.LogInformation("Calling CarianPermohonanJawatan");
        //    try
        //    {
        //        var data = await _permohonanjawatanext.CarianPermohonanJawatan(request);
        //        return Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        String err = "";
        //        if (ex != null)
        //        {
        //            _logger.LogError(ex, "Error in CarianPermohonanJawatan");
        //            if (ex.InnerException != null)
        //            {
        //                err = ex.InnerException.Message.ToString();
        //            }
        //        }

        //        return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
        //    }
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SenaraiPermohonanJawatanDto>>>SenaraiPermohonanJawatan(PermohonanJawatanCarianDto request)
        {
            _logger.LogInformation("Calling SenaraiPermohonanJawatan");
            try
            {
                var data = await _permohonanjawatanext.SenaraiPermohonanJawatan(request);
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
                     _logger.LogError(ex, "Error in SenaraiPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult> DaftarPermohonanJawatan([FromBody] PermohonanJawatanDaftarDto request)
        {
            _logger.LogInformation("Calling DaftarPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.DaftarPermohonanJawatan(request);
                return Ok(new { message = "Berjaya Daftar Permohonan Jawatan" });
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

        [HttpPatch("ulasan-status")]
        public async Task<ActionResult> KemaskiniUlasanStatusPermohonanJawatan([FromBody] UlasanRequestDto request)
        {
            _logger.LogInformation("Calling DaftarPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.KemaskiniUlasanPermohonanJawatan(request);
                return CreatedAtAction(nameof(KemaskiniUlasanPermohonanJawatan), new { request }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in KemaskiniUlasanPermohonanJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
            }
        }
        [HttpPatch("hantar")]
        public async Task<ActionResult> HantarPermohonanJawatan([FromBody] HantarRequestDto request)
        {
            _logger.LogInformation("Calling HantarPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.HantarPermohonanJawatan(request);
                return CreatedAtAction(nameof(HantarPermohonanJawatan), new { request }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in HantarPermohonanJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new { status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : "" });
            }
        }
        [HttpPatch("Ulasan-status-jenis")]
        public async Task<ActionResult> KemaskiniUlasanStatusJenisPermohonanJawatan([FromBody] UlasanStatusJenisRequestDto request)
        {
            _logger.LogInformation("Calling DaftarPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.KemaskiniUlasanStatusJenisPermohonanJawatan(request);
                return CreatedAtAction(nameof(KemaskiniUlasanStatusJenisPermohonanJawatan), new { request }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in KemaskiniUlasanPermohonanJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
            }
        }
        [HttpPatch("Ulasan-status-keputusan")]
        public async Task<ActionResult> KemaskiniUlasanStatusKeputusanPermohonanJawatan([FromBody] UlasanStatusKeputusanRequestDto request)
        {
            _logger.LogInformation("Calling DaftarPermohonanJawatan");
            try
            {
                var status = await _permohonanjawatanext.KemaskiniUlasanStatusKeputusanPermohonanJawatan(request);
                return Ok(new
                {
                    status = status == true ? "Berjaya":"Gagal",
                    message = status == true ? "Berjaya Kemaskini Ulasan Status Keputusan Permohonan Jawatan" : "Gagal Kemaskini Ulasan Status Keputusan Permohonan Jawatan"
                });
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in KemaskiniUlasanStatusKeputusanPermohonanJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new { status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : "" });
            }
        }
        [HttpPatch("Ulasan")]
        public async Task<ActionResult> KemaskiniUlasanPermohonanJawatan([FromBody] UlasanRequestDto request)
        {
            _logger.LogInformation("Calling DaftarPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.KemaskiniUlasanPermohonanJawatan(request);
                return CreatedAtAction(nameof(KemaskiniUlasanPermohonanJawatan), new { request }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in KemaskiniUlasanPermohonanJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
            }
        }
    }
}
