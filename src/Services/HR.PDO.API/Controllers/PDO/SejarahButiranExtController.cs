using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/sejarah-butiran")]
    public class SejarahButiranExtController : ControllerBase
    {
        private readonly ILogger<SejarahButiranExtController> _logger;
        private readonly ISejarahButiranExt _sejarahbutiranext;

        public SejarahButiranExtController(ISejarahButiranExt sejarahbutiranext, ILogger<SejarahButiranExtController> logger)
        {
            _sejarahbutiranext = sejarahbutiranext;
            _logger = logger;
        }

    }
}
