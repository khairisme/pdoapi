using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/rujukan/ruj-gred")]
    public class RujukanGredExtController : ControllerBase
    {
        private readonly ILogger<RujukanGredExtController> _logger;
        private readonly IRujukanGred _rujukangred;

        public RujukanGredExtController(IRujukanGred rujukangred, ILogger<RujukanGredExtController> logger)
        {
            _rujukangred = rujukangred;
            _logger = logger;
        }

        [HttpGet("klasifikasi/{IdKlasifikasiPerkhidmatan:int}/kumpulan/{IdKumpulanPerkhidmatan:int}")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanGredIkutKlasifikasiDanKumpulan(int IdKlasifikasiPerkhidmatan, int IdKumpulanPerkhidmatan)
        {
            _logger.LogInformation("Calling RujukanGredIkutKlasifikasiDanKumpulan");
            try
            {
                var data = await _rujukangred.RujukanGredIkutKlasifikasiDanKumpulan(IdKlasifikasiPerkhidmatan,IdKumpulanPerkhidmatan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanGredIkutKlasifikasiDanKumpulan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
