using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/kategori-jawatan")]
    public class RujukanKategoriJawatanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanKategoriJawatanExternalController> _logger;
        private readonly IRujKategoriJawatanExt _rujkategorijawatanext;

        public RujukanKategoriJawatanExternalController(IRujKategoriJawatanExt rujkategorijawatanext, ILogger<RujukanKategoriJawatanExternalController> logger)
        {
            _rujkategorijawatanext = rujkategorijawatanext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>>RujukanKategoriJawatan()
        {
            _logger.LogInformation("Calling RujukanKategoriJawatan");
            try
            {
                var data = await _rujkategorijawatanext.RujukanAgensi();
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
