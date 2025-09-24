using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/jenis-saraan")]
    public class RujukanJenisSaraanExtController : ControllerBase
    {
        private readonly ILogger<RujukanJenisSaraanExtController> _logger;
        private readonly IRujukanJenisSaraanExt _rujjenissaraanext;

        public RujukanJenisSaraanExtController(IRujukanJenisSaraanExt rujjenissaraanext, ILogger<RujukanJenisSaraanExtController> logger)
        {
            _rujjenissaraanext = rujjenissaraanext;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            try
            {
                _logger.LogInformation("Getting all RujJenisSaraan");

                var result = await _rujjenissaraanext.SenaraiJenisSaraan();

                return Ok(new
                {
                    status = result.Count() > 0 ? "Berjaya" : "Gagal",
                    items = result

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
