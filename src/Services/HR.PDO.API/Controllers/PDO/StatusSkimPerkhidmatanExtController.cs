using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/status-skim-perkhidmatan")]
    public class StatusSkimPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<StatusSkimPerkhidmatanExtController> _logger;
        private readonly IStatusSkimPerkhidmatanExt _statusskimperkhidmatanext;

        public StatusSkimPerkhidmatanExtController(IStatusSkimPerkhidmatanExt statusskimperkhidmatanext, ILogger<StatusSkimPerkhidmatanExtController> logger)
        {
            _statusskimperkhidmatanext = statusskimperkhidmatanext;
            _logger = logger;
        }

    }
}
