using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PPA;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-pangkat-badan-beruniform")]
    public class RujPangkatBadanBeruniformExtController : ControllerBase
    {
        private readonly ILogger<RujPangkatBadanBeruniformExtController> _logger;
        private readonly IRujPangkatBadanBeruniformExt _rujpangkatbadanberuniformext;

        public RujPangkatBadanBeruniformExtController(IRujPangkatBadanBeruniformExt rujpangkatbadanberuniformext, ILogger<RujPangkatBadanBeruniformExtController> logger)
        {
            _rujpangkatbadanberuniformext = rujpangkatbadanberuniformext;
            _logger = logger;
        }

    }
}
