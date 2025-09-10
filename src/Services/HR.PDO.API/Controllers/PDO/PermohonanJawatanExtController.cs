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

        [HttpGet("muat/{IdUnitOrganisasi}")]
        public async Task<ActionResult<MuatPermohonanJawatanOutputDto>> MuatPermohonanJawatan(int IdUnitOrganisasi)
        {
            _logger.LogInformation("Calling MuatPermohonanJawatan");
            try
            {
                var data = await _permohonanjawatanext.MuatPermohonanJawatan(IdUnitOrganisasi);
                return Ok(data);
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

                return StatusCode(500, ex.Message + "-" + err);
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

        [HttpPut("kemaskini-permohonan-jawatan/{Id}")]
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

        [HttpDelete("hapus-terus/{Id}")]
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
                var data = await _permohonanjawatanext.CarianPermohonanJawatan(request);
                return Ok(data); // empty list => 200 OK
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

        //        return StatusCode(500, ex.Message + "-" + err);
        //    }
        //}

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
        public async Task<ActionResult> DaftarPermohonanJawatan([FromBody] PermohonanJawatanDaftarDto request)
        {
            _logger.LogInformation("Calling DaftarPermohonanJawatan");
            try
            {
                await _permohonanjawatanext.DaftarPermohonanJawatan(request);
                return CreatedAtAction(nameof(DaftarPermohonanJawatan), new {request }, null);
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

        [HttpPatch("Ulasan-status")]
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

                return StatusCode(500, ex.Message + "-" + err);
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

                return StatusCode(500, ex.Message + "-" + err);
            }
        }
    }
}
