using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/urusan-perkhidmatan")]
    public class RujUrusanPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<RujUrusanPerkhidmatanExtController> _logger;
        private readonly IRujUrusanPerkhidmatanExt _rujurusanperkhidmatanext;

        public RujUrusanPerkhidmatanExtController(IRujUrusanPerkhidmatanExt rujurusanperkhidmatanext, ILogger<RujUrusanPerkhidmatanExtController> logger)
        {
            _rujurusanperkhidmatanext = rujurusanperkhidmatanext;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> RujukanUrusanPerkhidmatan()
        {
            try
            {
                _logger.LogInformation("Getting all RujukanUrusanPerkhidmatan");

                var data = await _rujurusanperkhidmatanext.RujukanUrusanPerkhidmatan();

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
                    _logger.LogError(ex, "Error in RujukanJenisJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

    }
}
