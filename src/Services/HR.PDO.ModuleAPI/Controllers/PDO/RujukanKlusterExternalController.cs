using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/kluster")]
    public class RujukanKlusterExternalController : ControllerBase
    {
        private readonly ILogger<RujukanKlusterExternalController> _logger;
        private readonly IRujKlusterExt _rujklusterext;

        public RujukanKlusterExternalController(IRujKlusterExt rujklusterext, ILogger<RujukanKlusterExternalController> logger)
        {
            _rujklusterext = rujklusterext;
            _logger = logger;
        }

    }
}
