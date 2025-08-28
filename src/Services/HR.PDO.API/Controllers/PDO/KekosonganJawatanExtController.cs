using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/kekosongan-jawatan")]
    public class KekosonganJawatanExtController : ControllerBase
    {
        private readonly ILogger<KekosonganJawatanExtController> _logger;
        private readonly IKekosonganJawatanExt _kekosonganjawatanext;

        public KekosonganJawatanExtController(IKekosonganJawatanExt kekosonganjawatanext, ILogger<KekosonganJawatanExtController> logger)
        {
            _kekosonganjawatanext = kekosonganjawatanext;
            _logger = logger;
        }

    }
}
