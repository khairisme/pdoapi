using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-jenis-agensi")]
    public class RujJenisAgensiExtController : ControllerBase
    {
        private readonly ILogger<RujJenisAgensiExtController> _logger;
        private readonly IRujJenisAgensiExt _rujjenisagensiext;

        public RujJenisAgensiExtController(IRujJenisAgensiExt rujjenisagensiext, ILogger<RujJenisAgensiExtController> logger)
        {
            _rujjenisagensiext = rujjenisagensiext;
            _logger = logger;
        }

    }
}
