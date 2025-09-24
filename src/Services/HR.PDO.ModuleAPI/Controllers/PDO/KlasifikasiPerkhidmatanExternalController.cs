using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.ModuleAPI.Controllers {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/klasifikasi-perkhidmatan")]
    public class KlasifikasiPerkhidmatanExternalController : ControllerBase
    {
        private readonly ILogger<KlasifikasiPerkhidmatanExternalController> _logger;
        private readonly IKlasifikasiPerkhidmatanExt _klasifikasiperkhidmatanext;

        public KlasifikasiPerkhidmatanExternalController(IKlasifikasiPerkhidmatanExt klasifikasiperkhidmatanext, ILogger<KlasifikasiPerkhidmatanExternalController> logger)
        {
            _klasifikasiperkhidmatanext = klasifikasiperkhidmatanext;
            if (_klasifikasiperkhidmatanext == null)
                Console.WriteLine("DI cannot resolve IKlasifikasiPerkhidmatanExt");

            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanKlasifikasiPerkhidmatan()
        {
            _logger.LogInformation("Calling RujukanKlasifikasiPerkhidmatan");
            try
            {
                var data = await _klasifikasiperkhidmatanext.RujukanKlasifikasiPerKhidmatan();
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
