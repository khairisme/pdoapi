using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/jenis-agensi")]
    public class RujukanJenisAgensiExternalController : ControllerBase
    {
        private readonly ILogger<RujukanJenisAgensiExternalController> _logger;
        private readonly IRujukanJenisAgensiExt _rujjenisagensiext;

        public RujukanJenisAgensiExternalController(IRujukanJenisAgensiExt rujjenisagensiext, ILogger<RujukanJenisAgensiExternalController> logger)
        {
            _rujjenisagensiext = rujjenisagensiext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> RujukanJenisAgensi()
        {
            _logger.LogInformation("Calling RujukanGelaranJawatan");
            try
            {
                var data = await _rujjenisagensiext.RujukanJenisAgensi();
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
                    _logger.LogError(ex, "Error in RujukanGelaranJawatan");
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
