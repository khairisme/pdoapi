using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/klasifikasi-perkhidmatan")]
    public class KlasifikasiPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<KlasifikasiPerkhidmatanExtController> _logger;
        private readonly IKlasifikasiPerkhidmatanExt _klasifikasiperkhidmatanext;

        public KlasifikasiPerkhidmatanExtController(IKlasifikasiPerkhidmatanExt klasifikasiperkhidmatanext, ILogger<KlasifikasiPerkhidmatanExtController> logger)
        {
            _klasifikasiperkhidmatanext = klasifikasiperkhidmatanext;
            _logger = logger;
        }

        [HttpGet("ruj")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanKlasifikasiPerkhidmatan()
        {
            _logger.LogInformation("Calling RujukanKlasifikasiPerkhidmatan");
            try
            {
                var data = await _klasifikasiperkhidmatanext.RujukanKlasifikasiPerKhidmatan();
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

    }
}
