using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-gelaran-jawatan")]
    public class RujGelaranJawatanExtController : ControllerBase
    {
        private readonly ILogger<RujGelaranJawatanExtController> _logger;
        private readonly IRujGelaranJawatanExt _rujgelaranjawatanext;

        public RujGelaranJawatanExtController(IRujGelaranJawatanExt rujgelaranjawatanext, ILogger<RujGelaranJawatanExtController> logger)
        {
            _rujgelaranjawatanext = rujgelaranjawatanext;
            _logger = logger;
        }

    }
}
