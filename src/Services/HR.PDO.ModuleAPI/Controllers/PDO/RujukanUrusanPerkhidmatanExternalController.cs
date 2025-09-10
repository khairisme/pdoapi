using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/urusan-perkhidmatan")]
    public class RujukanUrusanPerkhidmatanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanUrusanPerkhidmatanExternalController> _logger;
        private readonly IRujUrusanPerkhidmatanExt _rujurusanperkhidmatanext;

        public RujukanUrusanPerkhidmatanExternalController(IRujUrusanPerkhidmatanExt rujurusanperkhidmatanext, ILogger<RujukanUrusanPerkhidmatanExternalController> logger)
        {
            _rujurusanperkhidmatanext = rujurusanperkhidmatanext;
            _logger = logger;
        }

    }
}
