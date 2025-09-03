using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/soalan-lazim")]
    public class SoalanLazimExtController : ControllerBase
    {
        private readonly ILogger<SoalanLazimExtController> _logger;
        private readonly ISoalanLazimExt _soalanlazimext;

        public SoalanLazimExtController(ISoalanLazimExt soalanlazimext, ILogger<SoalanLazimExtController> logger)
        {
            _soalanlazimext = soalanlazimext;
            _logger = logger;
        }

    }
}
