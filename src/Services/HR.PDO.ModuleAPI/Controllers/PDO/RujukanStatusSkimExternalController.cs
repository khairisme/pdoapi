using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/status-skim")]
    public class RujukanStatusSkimExternalController : ControllerBase
    {
        private readonly ILogger<RujukanStatusSkimExternalController> _logger;
        private readonly IRujStatusSkimExt _rujstatusskimext;

        public RujukanStatusSkimExternalController(IRujStatusSkimExt rujstatusskimext, ILogger<RujukanStatusSkimExternalController> logger)
        {
            _rujstatusskimext = rujstatusskimext;
            _logger = logger;
        }

    }
}
