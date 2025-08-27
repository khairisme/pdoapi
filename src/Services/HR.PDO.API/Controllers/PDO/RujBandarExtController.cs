using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PPA;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-bandar")]
    public class RujBandarExtController : ControllerBase
    {
        private readonly ILogger<RujBandarExtController> _logger;
        private readonly IRujBandarExt _rujbandarext;

        public RujBandarExtController(IRujBandarExt rujbandarext, ILogger<RujBandarExtController> logger)
        {
            _rujbandarext = rujbandarext;
            _logger = logger;
        }

    }
}
