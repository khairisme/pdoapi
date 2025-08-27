using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-urusan-perkhidmatan")]
    public class RujUrusanPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<RujUrusanPerkhidmatanExtController> _logger;
        private readonly IRujUrusanPerkhidmatanExt _rujurusanperkhidmatanext;

        public RujUrusanPerkhidmatanExtController(IRujUrusanPerkhidmatanExt rujurusanperkhidmatanext, ILogger<RujUrusanPerkhidmatanExtController> logger)
        {
            _rujurusanperkhidmatanext = rujurusanperkhidmatanext;
            _logger = logger;
        }

    }
}
