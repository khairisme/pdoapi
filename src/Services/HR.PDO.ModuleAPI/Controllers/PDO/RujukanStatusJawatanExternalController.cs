using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/status-jawatan")]
    public class RujukanStatusJawatanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanStatusJawatanExternalController> _logger;
        private readonly IRujStatusJawatanExt _rujstatusjawatanext;

        public RujukanStatusJawatanExternalController(IRujStatusJawatanExt rujstatusjawatanext, ILogger<RujukanStatusJawatanExternalController> logger)
        {
            _rujstatusjawatanext = rujstatusjawatanext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>>RujukanStatusJawatan()
        {
            _logger.LogInformation("Calling RujukanStatusJawatan");
            try
            {
                var data = await _rujstatusjawatanext.RujukanStatusJawatan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
