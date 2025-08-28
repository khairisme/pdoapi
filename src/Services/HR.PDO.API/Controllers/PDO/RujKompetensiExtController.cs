using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-kompetensi")]
    public class RujKompetensiExtController : ControllerBase
    {
        private readonly ILogger<RujKompetensiExtController> _logger;
        private readonly IRujKompetensiExt _rujkompetensiext;

        public RujKompetensiExtController(IRujKompetensiExt rujkompetensiext, ILogger<RujKompetensiExtController> logger)
        {
            _rujkompetensiext = rujkompetensiext;
            _logger = logger;
        }

    }
}
