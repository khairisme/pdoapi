using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/status-kekosongan-jawatan")]
    public class RujukanStatusKekosonganJawatanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanStatusKekosonganJawatanExternalController> _logger;
        private readonly IRujStatusKekosonganJawatanExt _rujstatuskekosonganjawatanext;

        public RujukanStatusKekosonganJawatanExternalController(IRujStatusKekosonganJawatanExt rujstatuskekosonganjawatanext, ILogger<RujukanStatusKekosonganJawatanExternalController> logger)
        {
            _rujstatuskekosonganjawatanext = rujstatuskekosonganjawatanext;
            _logger = logger;
        }

    }
}
