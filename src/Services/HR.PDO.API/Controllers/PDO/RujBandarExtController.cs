using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/bandar")]
    public class RujBandarExtController : ControllerBase
    {
        private readonly ILogger<RujBandarExtController> _logger;
        private readonly IRujBandarExt _rujbandarext;

        public RujBandarExtController(IRujBandarExt rujbandarext, ILogger<RujBandarExtController> logger)
        {
            _rujbandarext = rujbandarext;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<object>> SenaraiBandar([FromQuery] BandarRequestDto request)
        {
            _logger.LogInformation("Calling SenaraiBandar");
            try
            {
                var data = await _rujbandarext.SenaraiBandar(request);
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
                    _logger.LogError(ex, "Error in SenaraiBandar");
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
