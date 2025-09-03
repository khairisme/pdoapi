using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/pengisian-jawatan")]
    public class PengisianJawatanExtController : ControllerBase
    {
        private readonly ILogger<PengisianJawatanExtController> _logger;
        private readonly IPengisianJawatanExt _pengisianjawatanext;

        public PengisianJawatanExtController(IPengisianJawatanExt pengisianjawatanext, ILogger<PengisianJawatanExtController> logger)
        {
            _pengisianjawatanext = pengisianjawatanext;
            _logger = logger;
        }

    }
}
