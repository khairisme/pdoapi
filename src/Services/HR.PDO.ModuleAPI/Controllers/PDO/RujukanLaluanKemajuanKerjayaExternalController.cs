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
        private readonly IRujLaluanKemajuanKerjayaExt _rujlaluankemajuankerjayaext;

        public RujukanLaluanKemajuanKerjayaExternalController(IRujLaluanKemajuanKerjayaExt rujlaluankemajuankerjayaext, ILogger<RujukanLaluanKemajuanKerjayaExternalController> logger)
        {
            _rujlaluankemajuankerjayaext = rujlaluankemajuankerjayaext;
            _logger = logger;
        }

    }
}
