using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/status-rekod")]
    public class RujukanStatusRekodExternalController : ControllerBase
    {
        private readonly ILogger<RujukanStatusRekodExternalController> _logger;
        private readonly IRujStatusRekodExt _rujstatusrekodext;

        public RujukanStatusRekodExternalController(IRujStatusRekodExt rujstatusrekodext, ILogger<RujukanStatusRekodExternalController> logger)
        {
            _rujstatusrekodext = rujstatusrekodext;
            _logger = logger;
        }

    }
}
