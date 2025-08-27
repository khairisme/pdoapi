using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-status-bekalan")]
    public class RujStatusBekalanExtController : ControllerBase
    {
        private readonly ILogger<RujStatusBekalanExtController> _logger;
        private readonly IRujStatusBekalanExt _rujstatusbekalanext;

        public RujStatusBekalanExtController(IRujStatusBekalanExt rujstatusbekalanext, ILogger<RujStatusBekalanExtController> logger)
        {
            _rujstatusbekalanext = rujstatusbekalanext;
            _logger = logger;
        }

    }
}
