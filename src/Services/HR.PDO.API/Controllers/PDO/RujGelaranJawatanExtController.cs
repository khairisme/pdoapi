using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/gelaran-jawatan")]
    public class RujGelaranJawatanExtController : ControllerBase
    {
        private readonly ILogger<RujGelaranJawatanExtController> _logger;
        private readonly IRujGelaranJawatanExt _rujgelaranjawatanext;

        public RujGelaranJawatanExtController(IRujGelaranJawatanExt rujgelaranjawatanext, ILogger<RujGelaranJawatanExtController> logger)
        {
            _rujgelaranjawatanext = rujgelaranjawatanext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>>RujukanGelaranJawatan()
        {
            _logger.LogInformation("Calling RujukanGelaranJawatan");
            try
            {
                var data = await _rujgelaranjawatanext.RujukanGelaranJawatan();
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
                     _logger.LogError(ex, "Error in RujukanGelaranJawatan");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<object>>> TambahGelaranJawatan(TambahGelaranJawatanRequestDto request)
        {
            _logger.LogInformation("Calling TambahGelaranJawatan");
            try
            {
                var data = await _rujgelaranjawatanext.TambahGelaranJawatan(request);
                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in TambahGelaranJawatan");
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
