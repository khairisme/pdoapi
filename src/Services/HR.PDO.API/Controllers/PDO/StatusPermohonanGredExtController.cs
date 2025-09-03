using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/status-permohonan-gred")]
    public class StatusPermohonanGredExtController : ControllerBase
    {
        private readonly ILogger<StatusPermohonanGredExtController> _logger;
        private readonly IStatusPermohonanGredExt _statuspermohonangredext;

        public StatusPermohonanGredExtController(IStatusPermohonanGredExt statuspermohonangredext, ILogger<StatusPermohonanGredExtController> logger)
        {
            _statuspermohonangredext = statuspermohonangredext;
            _logger = logger;
        }

    }
}
