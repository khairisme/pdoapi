using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/laluan-kemajuan-kerjaya")]
    public class RujLaluanKemajuanKerjayaExtController : ControllerBase
    {
        private readonly ILogger<RujLaluanKemajuanKerjayaExtController> _logger;
        private readonly IRujLaluanKemajuanKerjayaExt _rujlaluankemajuankerjayaext;

        public RujLaluanKemajuanKerjayaExtController(IRujLaluanKemajuanKerjayaExt rujlaluankemajuankerjayaext, ILogger<RujLaluanKemajuanKerjayaExtController> logger)
        {
            _rujlaluankemajuankerjayaext = rujlaluankemajuankerjayaext;
            _logger = logger;
        }

    }
}
