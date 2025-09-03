using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/kumpulan-perkhidmatan")]
    public class KumpulanPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<KumpulanPerkhidmatanExtController> _logger;
        private readonly IKumpulanPerkhidmatanExt _kumpulanperkhidmatanext;

        public KumpulanPerkhidmatanExtController(IKumpulanPerkhidmatanExt kumpulanperkhidmatanext, ILogger<KumpulanPerkhidmatanExtController> logger)
        {
            _kumpulanperkhidmatanext = kumpulanperkhidmatanext;
            _logger = logger;
        }

        [HttpGet("ruj")]
        public async Task<ActionResult<IEnumerable<object>>>RujukanKumpulanPerkhidmatan()
        {
            _logger.LogInformation("Calling RujukanKumpulanPerkhidmatan");
            try
            {
                var data = await _kumpulanperkhidmatanext.RujukanKumpulanPerkhidmatan();
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
