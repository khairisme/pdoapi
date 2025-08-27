using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/rujukan")]
    public class RujukanExtController : ControllerBase
    {
        private readonly ILogger<RujukanExtController> _logger;
        private readonly IRujukanExt _rujukanext;

        public RujukanExtController(IRujukanExt rujukanext, ILogger<RujukanExtController> logger)
        {
            _rujukanext = rujukanext;
            _logger = logger;
        }

        [HttpGet("ruj-klasifikasi-perkhidmatan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanKlasifikasiPerkhidmatan()
        {
            _logger.LogInformation("Calling RujukanKlasifikasiPerkhidmatan");
            try
            {
                var data = await _rujukanext.RujukanKlasifikasiPerKhidmatan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanKlasifikasiPerKhidmatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-jenis-jawatan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanJenisJawatan()
        {
            _logger.LogInformation("Calling RujukanJenisJawatan");
            try
            {
                var data = await _rujukanext.RujukanJenisJawatan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanJenisJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-gelaran-jawatan")]
        public async Task<ActionResult<IEnumerable<object>>>RujukanGelaranJawatan()
        {
            _logger.LogInformation("Calling RujukanGelaranJawatan");
            try
            {
                var data = await _rujukanext.RujukanGelaranJawatan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanGelaranJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-pangkat")]
        public async Task<ActionResult<IEnumerable<object>>>RujukanPangkat()
        {
            _logger.LogInformation("Calling RujukanPangkat");
            try
            {
                var data = await _rujukanext.RujukanPangkat();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanPangkat");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-agensi")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanAgensi()
        {
            _logger.LogInformation("Calling RujukanAgensi");
            try
            {
                var data = await _rujukanext.RujukanAgensi();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanAgensi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-status-permohonan-jawatan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusPermohonanJawatan(string KodRujPeranan)
        {
            _logger.LogInformation("Calling RujukanStatusPermohonanJawatan");
            try
            {
                var data = await _rujukanext.RujukanStatusPermohonanJawatan(KodRujPeranan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusPermohonanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-jenis-permohonan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanJenisPermohonan()
        {
            _logger.LogInformation("Calling RujukanJenisPermohonan");
            try
            {
                var data = await _rujukanext.RujukanJenisPermohonan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanJenisPermohonan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-status-kelulusan-jawatan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusKelulusanJawatan()
        {
            _logger.LogInformation("Calling RujukanStatusKelulusanJawatan");
            try
            {
                var data = await _rujukanext.RujukanStatusKelulusanJawatan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusKelulusanJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-status-semakan-poa-pwn")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusSemakanPOAPWN()
        {
            _logger.LogInformation("Calling RujukanStatusSemakanPOAPWN");
            try
            {
                var data = await _rujukanext.RujukanStatusSemakanPOAPWN();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusSemakanPOAPWN");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-status-persetujuan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusPersetujuan()
        {
            _logger.LogInformation("Calling RujukanStatusPersetujuan");
            try
            {
                var data = await _rujukanext.RujukanStatusPersetujuan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusPersetujuan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-status-pindaan-wpskp")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusPindaanWPSKP()
        {
            _logger.LogInformation("Calling RujukanStatusPindaanWPSKP");
            try
            {
                var data = await _rujukanext.RujukanStatusPindaanWPSKP();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusPindaanWPSKP");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-status-lengkap")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusLengkap()
        {
            _logger.LogInformation("Calling RujukanStatusLengkap");
            try
            {
                var data = await _rujukanext.RujukanStatusLengkap();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusLengkap");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-status-pindaan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusPindaan()
        {
            _logger.LogInformation("Calling RujukanStatusPindaan");
            try
            {
                var data = await _rujukanext.RujukanStatusPindaan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusPindaan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-status-sokongan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusSokongan()
        {
            _logger.LogInformation("Calling RujukanStatusSokongan");
            try
            {
                var data = await _rujukanext.RujukanStatusSokongan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusSokongan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-status-semakan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusSemakan()
        {
            _logger.LogInformation("Calling RujukanStatusSemakan");
            try
            {
                var data = await _rujukanext.RujukanStatusSemakan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusSemakan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-status-kajian-semula")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusKajianSemula()
        {
            _logger.LogInformation("Calling RujukanStatusKajianSemula");
            try
            {
                var data = await _rujukanext.RujukanStatusKajianSemula();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusKajianSemula");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-status-perakuan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanStatusPerakuan()
        {
            _logger.LogInformation("Calling RujukanStatusPerakuan");
            try
            {
                var data = await _rujukanext.RujukanStatusPerakuan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusPerakuan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-jenis-agensi")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanJenisAgensi()
        {
            _logger.LogInformation("Calling RujukanJenisAgensi");
            try
            {
                var data = await _rujukanext.RujukanJenisAgensi();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanJenisAgensi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-jenis-dokumen")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanJenisDokumen()
        {
            _logger.LogInformation("Calling RujukanJenisDokumen");
            try
            {
                var data = await _rujukanext.RujukanJenisDokumen();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanJenisDokumen");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-pasukan-perunding")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanPasukanPerunding()
        {
            _logger.LogInformation("Calling RujukanPasukanPerunding");
            try
            {
                var data = await _rujukanext.RujukanPasukanPerunding();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanPasukanPerunding");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-ketua-perkhidmatan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanKetuaPerkhidmatan(int IdSkimPerkhidmatan)
        {
            _logger.LogInformation("Calling RujukanKetuaPerkhidmatan");
            try
            {
                var data = await _rujukanext.RujukanKetuaPerkhidmatan(IdSkimPerkhidmatan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanKetuaPerkhidmatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-urusan-perkhidmatan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanUrusanPerkhidmatan()
        {
            _logger.LogInformation("Calling RujukanUrusanPerkhidmatan");
            try
            {
                var data = await _rujukanext.RujukanUrusanPerkhidmatan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanUrusanPerkhidmatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-jenis-mesyuarat")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanJenisMesyuarat()
        {
            _logger.LogInformation("Calling RujukanJenisMesyuarat");
            try
            {
                var data = await _rujukanext.RujukanJenisMesyuarat();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanJenisMesyuarat");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-kategori-unit-organisasi")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanKategoriUnitOrganisasi()
        {
            _logger.LogInformation("Calling RujukanKategoriUnitOrganisasi");
            try
            {
                var data = await _rujukanext.RujukanKategoriUnitOrganisasi();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanKategoriUnitOrganisasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("ruj-kumpulan-perkhidmatan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanKumpulanPerkhidmatan()
        {
            _logger.LogInformation("Calling RujukanKumpulanPerkhidmatan");
            try
            {
                var data = await _rujukanext.RujukanKumpulanPerkhidmatan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanKumpulanPerkhidmatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
