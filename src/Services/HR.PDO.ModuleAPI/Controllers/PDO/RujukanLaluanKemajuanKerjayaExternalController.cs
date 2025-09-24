using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/laluan-kemajuan-kerjaya")]
    public class RujukanLaluanKemajuanKerjayaExternalController : ControllerBase
    {
        private readonly ILogger<RujukanLaluanKemajuanKerjayaExternalController> _logger;
        private readonly IRujukanLaluanKemajuanKerjayaExt _rujlaluankemajuankerjayaext;

        public RujukanLaluanKemajuanKerjayaExternalController(IRujukanLaluanKemajuanKerjayaExt rujlaluankemajuankerjayaext, ILogger<RujukanLaluanKemajuanKerjayaExternalController> logger)
        {
            _rujlaluankemajuankerjayaext = rujlaluankemajuankerjayaext;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            _logger.LogInformation("Getting all RujukanLaluanKemajuanKerja");

            var result = await _rujlaluankemajuankerjayaext.RujukanLaluanKemajuanKerjaya();

            return Ok(new
            {
                status = result.Count() > 0 ? "Berjaya" : "Gagal",
                items = result

            });
        }

    }
}
