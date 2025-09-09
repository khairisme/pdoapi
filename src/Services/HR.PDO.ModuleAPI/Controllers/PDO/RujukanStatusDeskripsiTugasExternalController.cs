using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/status-deskripsi-tugas")]
    public class RujukanStatusDeskripsiTugasExternalController : ControllerBase
    {
        private readonly ILogger<RujukanStatusDeskripsiTugasExternalController> _logger;
        private readonly IRujStatusDeskripsiTugasExt _rujstatusdeskripsitugasext;

        public RujukanStatusDeskripsiTugasExternalController(IRujStatusDeskripsiTugasExt rujstatusdeskripsitugasext, ILogger<RujukanStatusDeskripsiTugasExternalController> logger)
        {
            _rujstatusdeskripsitugasext = rujstatusdeskripsitugasext;
            _logger = logger;
        }

    }
}
