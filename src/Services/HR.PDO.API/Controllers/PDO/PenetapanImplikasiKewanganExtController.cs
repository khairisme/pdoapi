using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/penetapan-implikasi-kewangan")]
    public class PenetapanImplikasiKewanganExtController : ControllerBase
    {
        private readonly ILogger<PenetapanImplikasiKewanganExtController> _logger;
        private readonly IPenetapanImplikasiKewanganExt _penetapanimplikasikewanganext;

        public PenetapanImplikasiKewanganExtController(IPenetapanImplikasiKewanganExt penetapanimplikasikewanganext, ILogger<PenetapanImplikasiKewanganExtController> logger)
        {
            _penetapanimplikasikewanganext = penetapanimplikasikewanganext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<object>>>KosImplikasiKewangan([FromBody] ImplikasiKewanganRequestDto request)
        {
            _logger.LogInformation("Calling KosImplikasiKewangan");
            try
            {
                var data = await _penetapanimplikasikewanganext.KosImplikasiKewangan(request);
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
                     _logger.LogError(ex, "Error in KosImplikasiKewangan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }
        [HttpPost("senarai")]
        public async Task<ActionResult<IEnumerable<object>>> SenaraiImplikasiKewangan([FromBody] SenaraiImplikasiKewanganRequestDto request)
        {
            _logger.LogInformation("Calling KosImplikasiKewangan");
            try
            {
                var data = await _penetapanimplikasikewanganext.SenaraiImplikasiKewangan(request);
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
                    _logger.LogError(ex, "Error in KosImplikasiKewangan");
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
