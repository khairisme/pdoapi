using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/skim-ketua-perkhidmatan")]
    public class SkimKetuaPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<SkimKetuaPerkhidmatanExtController> _logger;
        private readonly ISkimKetuaPerkhidmatanExt _skimketuaperkhidmatanext;

        public SkimKetuaPerkhidmatanExtController(ISkimKetuaPerkhidmatanExt skimketuaperkhidmatanext, ILogger<SkimKetuaPerkhidmatanExtController> logger)
        {
            _skimketuaperkhidmatanext = skimketuaperkhidmatanext;
            _logger = logger;
        }

        [HttpGet("ruj")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanKetuaPerkhidmatan(int IdSkimPerkhidmatan)
        {
            _logger.LogInformation("Calling RujukanKetuaPerkhidmatan");
            try
            {
                var data = await _skimketuaperkhidmatanext.RujukanKetuaPerkhidmatan(IdSkimPerkhidmatan);
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

        [HttpPost("tambah")]
        public async Task<ActionResult> TambahKetuaPerkhidmatan([FromQuery] Guid UserId,[FromBody] SkimKetuaPerkhidmatanRequestDto request)
        {
            _logger.LogInformation("Calling RujukanKetuaPerkhidmatan");
            try
            {
                await _skimketuaperkhidmatanext.TambahKetuaPerkhidmatan(UserId, request);
                return CreatedAtAction(nameof(TambahKetuaPerkhidmatan), new { UserId, request }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in TambahKetuaPerkhidmatan");
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
