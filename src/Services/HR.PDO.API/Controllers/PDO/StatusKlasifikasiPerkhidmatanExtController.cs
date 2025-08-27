using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/status-klasifikasi-perkhidmatan")]
    public class StatusKlasifikasiPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<StatusKlasifikasiPerkhidmatanExtController> _logger;
        private readonly IStatusKlasifikasiPerkhidmatanExt _statusklasifikasiperkhidmatanext;

        public StatusKlasifikasiPerkhidmatanExtController(IStatusKlasifikasiPerkhidmatanExt statusklasifikasiperkhidmatanext, ILogger<StatusKlasifikasiPerkhidmatanExtController> logger)
        {
            _statusklasifikasiperkhidmatanext = statusklasifikasiperkhidmatanext;
            _logger = logger;
        }

    }
}
