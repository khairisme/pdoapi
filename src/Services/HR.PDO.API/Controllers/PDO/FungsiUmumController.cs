using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/fungsi-umum")]
    public class FungsiUmumController : ControllerBase
    {
        private readonly ILogger<FungsiUmumController> _logger;
        private readonly IFungsiUmum _fungsiumum;

        public FungsiUmumController(IFungsiUmum fungsiumum, ILogger<FungsiUmumController> logger)
        {
            _fungsiumum = fungsiumum;
            _logger = logger;
        }

        [HttpGet("jana-nombor-rujukan/{id}")]
        public async Task<ActionResult<DropDownDto>> JanaNomborRujukan(int IdUnitOrganisasi)
        {
            _logger.LogInformation("Calling JanaNomborRujukan");
            try
            {
                var data = await _fungsiumum.JanaNomborRujukan(IdUnitOrganisasi);
                return Ok(data);
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in JanaNomborRujukan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
