using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-status-kekosongan-jawatan")]
    public class RujStatusKekosonganJawatanExtController : ControllerBase
    {
        private readonly ILogger<RujStatusKekosonganJawatanExtController> _logger;
        private readonly IRujStatusKekosonganJawatanExt _rujstatuskekosonganjawatanext;

        public RujStatusKekosonganJawatanExtController(IRujStatusKekosonganJawatanExt rujstatuskekosonganjawatanext, ILogger<RujStatusKekosonganJawatanExtController> logger)
        {
            _rujstatuskekosonganjawatanext = rujstatuskekosonganjawatanext;
            _logger = logger;
        }

    }
}
