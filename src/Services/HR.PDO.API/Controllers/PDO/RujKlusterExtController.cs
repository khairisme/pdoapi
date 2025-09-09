using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/kluster")]
    public class RujKlusterExtController : ControllerBase
    {
        private readonly ILogger<RujKlusterExtController> _logger;
        private readonly IRujKlusterExt _rujklusterext;

        public RujKlusterExtController(IRujKlusterExt rujklusterext, ILogger<RujKlusterExtController> logger)
        {
            _rujklusterext = rujklusterext;
            _logger = logger;
        }

    }
}
