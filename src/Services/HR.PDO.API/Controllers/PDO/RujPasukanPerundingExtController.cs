using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-pasukan-perunding")]
    public class RujPasukanPerundingExtController : ControllerBase
    {
        private readonly ILogger<RujPasukanPerundingExtController> _logger;
        private readonly IRujPasukanPerundingExt _rujpasukanperundingext;

        public RujPasukanPerundingExtController(IRujPasukanPerundingExt rujpasukanperundingext, ILogger<RujPasukanPerundingExtController> logger)
        {
            _rujpasukanperundingext = rujpasukanperundingext;
            _logger = logger;
        }

    }
}
