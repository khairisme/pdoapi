using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/ruj-status-deskripsi-tugas")]
    public class RujStatusDeskripsiTugasExtController : ControllerBase
    {
        private readonly ILogger<RujStatusDeskripsiTugasExtController> _logger;
        private readonly IRujStatusDeskripsiTugasExt _rujstatusdeskripsitugasext;

        public RujStatusDeskripsiTugasExtController(IRujStatusDeskripsiTugasExt rujstatusdeskripsitugasext, ILogger<RujStatusDeskripsiTugasExtController> logger)
        {
            _rujstatusdeskripsitugasext = rujstatusdeskripsitugasext;
            _logger = logger;
        }

    }
}
