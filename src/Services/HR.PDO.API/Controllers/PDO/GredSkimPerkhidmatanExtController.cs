using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/gred-skim-perkhidmatan")]
    public class GredSkimPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<GredSkimPerkhidmatanExtController> _logger;
        private readonly IGredSkimPerkhidmatanExt _gredskimperkhidmatanext;

        public GredSkimPerkhidmatanExtController(IGredSkimPerkhidmatanExt gredskimperkhidmatanext, ILogger<GredSkimPerkhidmatanExtController> logger)
        {
            _gredskimperkhidmatanext = gredskimperkhidmatanext;
            _logger = logger;
        }

    }
}
