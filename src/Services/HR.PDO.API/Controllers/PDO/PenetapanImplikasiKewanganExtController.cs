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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>>SenaraiImplikasiKewangan(int IdPermohonanJawatan)
        {
            _logger.LogInformation("Calling SenaraiImplikasiKewangan");
            try
            {
                var data = await _penetapanimplikasikewanganext.SenaraiImplikasiKewangan(IdPermohonanJawatan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiImplikasiKewangan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
