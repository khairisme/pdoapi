using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/rujukan/skim-perkhidmatan")]
    public class RujukanSkimPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<RujukanSkimPerkhidmatanExtController> _logger;
        private readonly IRujukanSkimPerkhidmatan _rujukanskimperkhidmatan;

        public RujukanSkimPerkhidmatanExtController(IRujukanSkimPerkhidmatan rujukanskimperkhidmatan, ILogger<RujukanSkimPerkhidmatanExtController> logger)
        {
            _rujukanskimperkhidmatan = rujukanskimperkhidmatan;
            _logger = logger;
        }

        [HttpGet("klasifikasi/{IdKlasifikasiPerkhidmatan:int}/kumpulan/{IdKumpulanPerkhidmatan:int}")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan(int IdKlasifikasiPerkhidmatan, int IdKumpulanPerkhidmatan)
        {
            _logger.LogInformation("Calling RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan");
            try
            {
                var data = await _rujukanskimperkhidmatan.RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan(IdKlasifikasiPerkhidmatan,IdKumpulanPerkhidmatan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanSkimPerkhidmatan()
        {
            _logger.LogInformation("Calling RujukanSkimPerkhidmatan");
            try
            {
                var data = await _rujukanskimperkhidmatan.RujukanSkimPerkhidmatan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanSkimPerkhidmatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("klasifikasi/{IdKlasifikasiPerkhidmatan:int}")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanSkimPerkhidmatanIkutKlasifikasi(int IdKlasifikasiPerkhidmatan)
        {
            _logger.LogInformation("Calling RujukanSkimPerkhidmatanIkutKlasifikasi");
            try
            {
                var data = await _rujukanskimperkhidmatan.RujukanSkimPerkhidmatanIkutKlasifikasi(IdKlasifikasiPerkhidmatan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanSkimPerkhidmatanIkutKlasifikasi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

        [HttpGet("kumpulan/{IdKumpulanPerkhidmatan:int}")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanSkimPerkhidmatanIkutKumpulan(int  IdKumpulanPerkhidmatan)
        {
            _logger.LogInformation("Calling RujukanSkimPerkhidmatanIkutKumpulan");
            try
            {
                var data = await _rujukanskimperkhidmatan.RujukanSkimPerkhidmatanIkutKumpulan( IdKumpulanPerkhidmatan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanSkimPerkhidmatanIkutKumpulan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
