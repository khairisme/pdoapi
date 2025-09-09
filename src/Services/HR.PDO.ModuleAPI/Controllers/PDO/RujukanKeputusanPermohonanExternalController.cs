using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/keputusan-permohonan")]
    public class RujukanKeputusanPermohonanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanKeputusanPermohonanExternalController> _logger;
        private readonly IRujKeputusanPermohonanExt _rujkeputusanpermohonanext;

        public RujukanKeputusanPermohonanExternalController(IRujKeputusanPermohonanExt rujkeputusanpermohonanext, ILogger<RujukanKeputusanPermohonanExternalController> logger)
        {
            _rujkeputusanpermohonanext = rujkeputusanpermohonanext;
            _logger = logger;
        }

    }
}
