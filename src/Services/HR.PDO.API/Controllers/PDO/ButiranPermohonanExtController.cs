using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/butiran-permohonan")]
    public class ButiranPermohonanExtController : ControllerBase
    {
        private readonly ILogger<ButiranPermohonanExtController> _logger;
        private readonly IButiranPermohonanExt _butiranpermohonanext;

        public ButiranPermohonanExtController(IButiranPermohonanExt butiranpermohonanext, ILogger<ButiranPermohonanExtController> logger)
        {
            _butiranpermohonanext = butiranpermohonanext;
            _logger = logger;
        }

        [HttpPost("tambah")]
        public async Task<ActionResult> TambahButiranPermohonan([FromQuery] Guid UserId, TambahButiranPermohonanDto request)
        {
            _logger.LogInformation("Calling TambahButiranPermohonan");
            try
            {
                await _butiranpermohonanext.TambahButiranPermohonan(UserId,request);
                return CreatedAtAction(nameof(TambahButiranPermohonan), new {UserId, request }, null);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in TambahButiranPermohonan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpPost("kemas-kini")]
        public async Task<ActionResult> KemaskiniButiranPermohonan([FromQuery] Guid UserId, KemaskiniButiranPermohonanRequestDto request)
        {
            _logger.LogInformation("Calling KemaskiniButiranPermohonan");
            try
            {
                await _butiranpermohonanext.KemaskiniButiranPermohonan(UserId, request);
                return CreatedAtAction(nameof(KemaskiniButiranPermohonan), new { UserId, request }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in KemaskiniButiranPermohonan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        [HttpPost("mansuh-cadangan-jawatan")]
        public async Task<ActionResult> MansuhButiranPermohonanCadanganJawatan([FromQuery] Guid UserId, int IdButiranPermohonan, int IdCadanganJawatan)
        {
            _logger.LogInformation("Calling MansuhButiranPermohonanCadanganJawatan");
            try
            {
                await _butiranpermohonanext.MansuhButiranPermohonanCadanganJawatan(UserId, IdButiranPermohonan, IdCadanganJawatan);
                return CreatedAtAction(nameof(MansuhButiranPermohonanCadanganJawatan), new { UserId, IdCadanganJawatan }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in MansuhButiranPermohonanCadanganJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        [HttpPost("kemas-kini-butir-perubahan")]
        public async Task<ActionResult> KemaskiniButiranPerubahanButiranPermohonan([FromQuery] Guid UserId, KemaskiniButiranPermohonanRequestDto request)
        {
            _logger.LogInformation("Calling KemaskiniButiranPerubahanButiranPermohonan");
            try
            {
                await _butiranpermohonanext.KemaskiniButiranPerubahanButiranPermohonan(UserId, request);
                return CreatedAtAction(nameof(KemaskiniButiranPerubahanButiranPermohonan), new { UserId, request }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in TambahButiranPermohonan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }


        [HttpPatch("kira-implikasi-kewangan")]
        public async Task<ActionResult> KiraImplikasiKewanganButiranPermohonan([FromQuery] Guid UserId, KiraImplikasiKewanganRequestDto request)
        {
            _logger.LogInformation("Calling TambahButiranPermohonan");
            try
            {
                await _butiranpermohonanext.KiraImplikasiKewanganButiranPermohonan(UserId, request);
                return CreatedAtAction(nameof(KiraImplikasiKewanganButiranPermohonan), new { UserId, request }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in TambahButiranPermohonan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TambahButiranPermohonanDto>>> SenaraiButiranPermohonan()
        {
            _logger.LogInformation("Calling TambahButiranPermohonan");
            try
            {
                var result = await _butiranpermohonanext.SenaraiButiranPermohonan();
                return result;
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in TambahButiranPermohonan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        [HttpGet("{IdPermohonanJawatan}")]
        public async Task<ActionResult<TambahButiranPermohonanDto>> BacaButiranPermohonan(int IdPermohonanJawatan)
        {
            _logger.LogInformation("Calling TambahButiranPermohonan");
            try
            {
                var result = await _butiranpermohonanext.BacaButiranPermohonan(IdPermohonanJawatan);
                return result;
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in TambahButiranPermohonan");
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
