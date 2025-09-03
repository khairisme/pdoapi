using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/jenis-jawatan")]
    public class RujJenisJawatanExtController : ControllerBase
    {
        private readonly ILogger<RujJenisJawatanExtController> _logger;
        private readonly IRujJenisJawatanExt _rujjenisjawatanext;

        public RujJenisJawatanExtController(IRujJenisJawatanExt rujjenisjawatanext, ILogger<RujJenisJawatanExtController> logger)
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
