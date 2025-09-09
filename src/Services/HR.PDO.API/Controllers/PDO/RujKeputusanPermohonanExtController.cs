using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/keputusan-permohonan")]
    public class RujKeputusanPermohonanExtController : ControllerBase
    {
        private readonly ILogger<RujKeputusanPermohonanExtController> _logger;
        private readonly IRujKeputusanPermohonanExt _rujkeputusanpermohonanext;

        public RujKeputusanPermohonanExtController(IRujKeputusanPermohonanExt rujkeputusanpermohonanext, ILogger<RujKeputusanPermohonanExtController> logger)
        {
            _rujkeputusanpermohonanext = rujkeputusanpermohonanext;
            _logger = logger;
        }

    }
}
