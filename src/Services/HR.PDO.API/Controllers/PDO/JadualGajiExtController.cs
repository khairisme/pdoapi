using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDP {
    [ApiController]
    [Route("api/pdo/jadual-gaji")]
    public class JadualGajiExtController : ControllerBase
    {
        private readonly ILogger<JadualGajiExtController> _logger;
        private readonly IJadualGajiExt _jadualgajiext;

        public JadualGajiExtController(IJadualGajiExt jadualgajiext, ILogger<JadualGajiExtController> logger)
        {
            _jadualgajiext = jadualgajiext;
            _logger = logger;
        }

        [HttpGet("senarai-semua")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>SenaraiSemuaJadualGaji(JadualGajiExtDto request)
        {
            _logger.LogInformation("Calling SenaraiSemuaJadualGaji");
            try
            {
                var data = await _jadualgajiext.SenaraiSemuaJadualGaji(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in SenaraiSemuaJadualGaji");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
