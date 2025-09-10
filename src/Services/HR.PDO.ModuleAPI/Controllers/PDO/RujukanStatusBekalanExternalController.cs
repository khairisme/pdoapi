using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/status-bekalan")]
    public class RujukanStatusBekalanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanStatusBekalanExternalController> _logger;
        private readonly IRujStatusBekalanExt _rujstatusbekalanext;

        public RujukanStatusBekalanExternalController(IRujStatusBekalanExt rujstatusbekalanext, ILogger<RujukanStatusBekalanExternalController> logger)
        {
            _rujstatusbekalanext = rujstatusbekalanext;
            _logger = logger;
        }

    }
}
