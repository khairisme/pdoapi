using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using  Shared.Contracts.DTOs;
using Swashbuckle.AspNetCore.Annotations;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/pasukan-perunding")]
    public class RujukanPasukanPerundingExternalController : ControllerBase
    {
        private readonly ILogger<RujukanPasukanPerundingExternalController> _logger;
        private readonly IRujukanPasukanPerundingExt _rujpasukanperundingext;

        public RujukanPasukanPerundingExternalController(IRujukanPasukanPerundingExt rujpasukanperundingext, ILogger<RujukanPasukanPerundingExternalController> logger)
        {
            _rujpasukanperundingext = rujpasukanperundingext;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanAgensi()
        {
            _logger.LogInformation("Calling RujukanAgensi");
            try
            {
                var data = await _rujpasukanperundingext.RujukanPasukanPerunding();
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
                    _logger.LogError(ex, "Error in RujukanAgensi");
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
