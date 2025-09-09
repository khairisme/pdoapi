using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/agensi")]
    public class RujukanAgensiExtController : ControllerBase
    {
        private readonly ILogger<RujukanAgensiExtController> _logger;
        private readonly IRujukanAgensiExt _rujukanagensiext;

        public RujukanAgensiExtController(IRujukanAgensiExt rujukanagensiext, ILogger<RujukanAgensiExtController> logger)
        {
            _rujukanagensiext = rujukanagensiext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanAgensi(string? NamaAgensi)
        {
            _logger.LogInformation("Calling RujukanAgensi");
            try
            {
                var data = await _rujukanagensiext.RujukanAgensi(NamaAgensi);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanAgensi");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
