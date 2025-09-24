using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/jawatan/rujukan")]
    public class JawatanExtController : ControllerBase
    {
        private readonly ILogger<JawatanExtController> _logger;
        private readonly IJawatanExt _JawatanExt;

        public JawatanExtController(IJawatanExt JawatanExt, ILogger<JawatanExtController> logger)
        {
            _JawatanExt = JawatanExt;
            _logger = logger;
        }

        [HttpPost("senarai")]
        public async Task<ActionResult<IEnumerable<SenaraiPermohonanJawatanDto>>>SenaraiJawatan([FromBody] ButiranJawatanRequestDto request)
        {
            _logger.LogInformation("Calling SenaraiJawatan");
            try
            {
                var data = await _JawatanExt.SenaraiJawatan(request);
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
                     _logger.LogError(ex, "Error in SenaraiJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
