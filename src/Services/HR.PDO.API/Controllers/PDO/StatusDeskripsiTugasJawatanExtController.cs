using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/status-deskripsi-tugas-jawatan")]
    public class StatusDeskripsiTugasJawatanExtController : ControllerBase
    {
        private readonly ILogger<StatusDeskripsiTugasJawatanExtController> _logger;
        private readonly IStatusDeskripsiTugasJawatanExt _statusdeskripsitugasjawatanext;

        public StatusDeskripsiTugasJawatanExtController(IStatusDeskripsiTugasJawatanExt statusdeskripsitugasjawatanext, ILogger<StatusDeskripsiTugasJawatanExtController> logger)
        {
            _statusdeskripsitugasjawatanext = statusdeskripsitugasjawatanext;
            _logger = logger;
        }

    }
}
