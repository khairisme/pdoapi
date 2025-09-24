using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using  Shared.Contracts.DTOs;
using Swashbuckle.AspNetCore.Annotations;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/kluster")]
    public class RujukanKlusterExternalController : ControllerBase
    {
        private readonly ILogger<RujukanKlusterExternalController> _logger;
        private readonly IRujukanKlusterExt _rujklusterext;

        public RujukanKlusterExternalController(IRujukanKlusterExt rujklusterext, ILogger<RujukanKlusterExternalController> logger)
        {
            _rujklusterext = rujklusterext;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanKluster()
        {
            _logger.LogInformation("Calling RujukanKluster");
            try
            {
                var data = await _rujklusterext.RujukanKluster();
                return Ok(new
                {
                    status = data.Count() > 0 ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in RujukanKluster");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
            }
        }

    }
}
