using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/status-permohonan")]
    public class RujukanStatusPermohonanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanStatusPermohonanExternalController> _logger;
        private readonly IRujStatusPermohonanExt _rujstatuspermohonanext;

        public RujukanStatusPermohonanExternalController(IRujStatusPermohonanExt rujstatuspermohonanext, ILogger<RujukanStatusPermohonanExternalController> logger)
        {
            _rujstatuspermohonanext = rujstatuspermohonanext;
            _logger = logger;
        }

    }
}
