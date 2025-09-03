using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/ruj-status-rekod")]
    public class RujStatusRekodExtController : ControllerBase
    {
        private readonly ILogger<RujStatusRekodExtController> _logger;
        private readonly IRujStatusRekodExt _rujstatusrekodext;

        public RujStatusRekodExtController(IRujStatusRekodExt rujstatusrekodext, ILogger<RujStatusRekodExtController> logger)
        {
            _rujstatusrekodext = rujstatusrekodext;
            _logger = logger;
        }

    }
}
