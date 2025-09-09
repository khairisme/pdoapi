using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/status-permohonan-pengisian")]
    public class RujukanStatusPermohonanPengisianExternalController : ControllerBase
    {
        private readonly ILogger<RujukanStatusPermohonanPengisianExternalController> _logger;
        private readonly IRujStatusPermohonanPengisianExt _rujstatuspermohonanpengisianext;

        public RujukanStatusPermohonanPengisianExternalController(IRujStatusPermohonanPengisianExt rujstatuspermohonanpengisianext, ILogger<RujukanStatusPermohonanPengisianExternalController> logger)
        {
            _rujstatuspermohonanpengisianext = rujstatuspermohonanpengisianext;
            _logger = logger;
        }

    }
}
