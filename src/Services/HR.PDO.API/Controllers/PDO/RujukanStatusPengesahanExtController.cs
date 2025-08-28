using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/rujukan")]
    public class RujukanStatusPengesahanExtController : ControllerBase
    {
        private readonly ILogger<RujukanStatusPengesahanExtController> _logger;
        private readonly IRujukanStatusPengesahan _rujukanstatuspengesahan;

        public RujukanStatusPengesahanExtController(IRujukanStatusPengesahan rujukanstatuspengesahan, ILogger<RujukanStatusPengesahanExtController> logger)
        {
            _rujukanstatuspengesahan = rujukanstatuspengesahan;
            _logger = logger;
        }

        [HttpGet("status-pengesahan")]
        public async Task<ActionResult<IEnumerable<object>>>RujukanStatusPengesahan()
        {
            _logger.LogInformation("Calling RujukanStatusPengesahan");
            try
            {
                var data = await _rujukanstatuspengesahan.RujukanStatusPengesahan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanStatusPengesahan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
