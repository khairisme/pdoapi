using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/gelaran-jawatan")]
    public class RujukanGelaranJawatanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanGelaranJawatanExternalController> _logger;
        private readonly IRujGelaranJawatanExt _rujgelaranjawatanext;

        public RujukanGelaranJawatanExternalController(IRujGelaranJawatanExt rujgelaranjawatanext, ILogger<RujukanGelaranJawatanExternalController> logger)
        {
            _rujgelaranjawatanext = rujgelaranjawatanext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>>RujukanGelaranJawatan()
        {
            _logger.LogInformation("Calling RujukanGelaranJawatan");
            try
            {
                var data = await _rujgelaranjawatanext.RujukanGelaranJawatan();
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

    }
}
