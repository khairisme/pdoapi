using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-status-skim")]
    public class RujStatusSkimExtController : ControllerBase
    {
        private readonly ILogger<RujStatusSkimExtController> _logger;
        private readonly IRujStatusSkimExt _rujstatusskimext;

        public RujStatusSkimExtController(IRujStatusSkimExt rujstatusskimext, ILogger<RujStatusSkimExtController> logger)
        {
            _rujstatusskimext = rujstatusskimext;
            _logger = logger;
        }

    }
}
