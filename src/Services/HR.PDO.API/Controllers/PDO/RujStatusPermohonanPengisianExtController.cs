using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-status-permohonan-pengisian")]
    public class RujStatusPermohonanPengisianExtController : ControllerBase
    {
        private readonly ILogger<RujStatusPermohonanPengisianExtController> _logger;
        private readonly IRujStatusPermohonanPengisianExt _rujstatuspermohonanpengisianext;

        public RujStatusPermohonanPengisianExtController(IRujStatusPermohonanPengisianExt rujstatuspermohonanpengisianext, ILogger<RujStatusPermohonanPengisianExtController> logger)
        {
            _rujstatuspermohonanpengisianext = rujstatuspermohonanpengisianext;
            _logger = logger;
        }

    }
}
