using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PPA;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/ruj-negeri")]
    public class RujNegeriExtController : ControllerBase
    {
        private readonly ILogger<RujNegeriExtController> _logger;
        private readonly IRujNegeriExt _rujnegeriext;

        public RujNegeriExtController(IRujNegeriExt rujnegeriext, ILogger<RujNegeriExtController> logger)
        {
            _rujnegeriext = rujnegeriext;
            _logger = logger;
        }

    }
}
