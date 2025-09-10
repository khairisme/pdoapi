using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/status-permohonan")]
    public class RujStatusPermohonanExtController : ControllerBase
    {
        private readonly ILogger<RujStatusPermohonanExtController> _logger;
        private readonly IRujStatusPermohonanExt _rujstatuspermohonanext;

        public RujStatusPermohonanExtController(IRujStatusPermohonanExt rujstatuspermohonanext, ILogger<RujStatusPermohonanExtController> logger)
        {
            _rujstatuspermohonanext = rujstatuspermohonanext;
            _logger = logger;
        }

    }
}
