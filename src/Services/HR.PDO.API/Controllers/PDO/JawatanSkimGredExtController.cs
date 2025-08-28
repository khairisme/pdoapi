using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/jawatan-skim-gred")]
    public class JawatanSkimGredExtController : ControllerBase
    {
        private readonly ILogger<JawatanSkimGredExtController> _logger;
        private readonly IJawatanSkimGredExt _jawatanskimgredext;

        public JawatanSkimGredExtController(IJawatanSkimGredExt jawatanskimgredext, ILogger<JawatanSkimGredExtController> logger)
        {
            _jawatanskimgredext = jawatanskimgredext;
            _logger = logger;
        }

    }
}
