using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/negeri")]
    public class RujNegeriExtController : ControllerBase
    {
        private readonly ILogger<RujNegeriExtController> _logger;
        private readonly IRujNegeriExt _rujnegeriext;

        public RujNegeriExtController(IRujNegeriExt rujnegeriext, ILogger<RujNegeriExtController> logger)
        {
            _rujnegeriext = rujnegeriext;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<object>> SenaraiNegeri([FromQuery] NegeriRequestDto request)
        {
            _logger.LogInformation("Calling SenaraiNegeri");
            try
            {
                var data = await _rujnegeriext.SenaraiNegeri(request);
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
                    _logger.LogError(ex, "Error in SenaraiNegeri");
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
