using HR.PDO.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using  Shared.Contracts.DTOs;
using Swashbuckle.AspNetCore.Annotations;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/negara")]
    public class RujNegaraExtController : ControllerBase
    {
        private readonly ILogger<RujNegaraExtController> _logger;
        private readonly IRujNegaraExt _rujnegaraext;

        public RujNegaraExtController(IRujNegaraExt rujnegaraext, ILogger<RujNegaraExtController> logger)
        {
            _rujnegaraext = rujnegaraext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<object>> SenaraiNegara()
        {
            _logger.LogInformation("Calling BacaUnitOrganisasi");
            try
            {
                var data = await _rujnegaraext.SenaraiNegara();
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
                    _logger.LogError(ex, "Error in BacaUnitOrganisasi");
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
