using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan")]
    public class RujukanExtController : ControllerBase
    {
        private readonly ILogger<RujukanExtController> _logger;
        private readonly IRujukanExt _rujukanext;

        public RujukanExtController(IRujukanExt rujukanext, ILogger<RujukanExtController> logger)
        {
            _rujukanext = rujukanext;
            _logger = logger;
        }

    }
}
