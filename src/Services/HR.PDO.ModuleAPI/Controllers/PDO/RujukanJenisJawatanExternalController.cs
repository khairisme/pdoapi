using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/jenis-jawatan")]
    public class RujukanJenisJawatanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanJenisJawatanExternalController> _logger;
        private readonly IRujJenisJawatanExt _rujjenisjawatanext;

        public RujukanJenisJawatanExternalController(IRujJenisJawatanExt rujjenisjawatanext, ILogger<RujukanJenisJawatanExternalController> logger)
        {
            _rujjenisjawatanext = rujjenisjawatanext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanJenisJawatan()
        {
            _logger.LogInformation("Calling RujukanJenisJawatan");
            try
            {
                var data = await _rujjenisjawatanext.RujukanJenisJawatan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanJenisJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
