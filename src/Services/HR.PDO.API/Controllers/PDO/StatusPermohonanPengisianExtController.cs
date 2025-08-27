using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/status-permohonan-pengisian")]
    public class StatusPermohonanPengisianExtController : ControllerBase
    {
        private readonly ILogger<StatusPermohonanPengisianExtController> _logger;
        private readonly IStatusPermohonanPengisianExt _statuspermohonanpengisianext;

        public StatusPermohonanPengisianExtController(IStatusPermohonanPengisianExt statuspermohonanpengisianext, ILogger<StatusPermohonanPengisianExtController> logger)
        {
            _statuspermohonanpengisianext = statuspermohonanpengisianext;
            _logger = logger;
        }

    }
}
