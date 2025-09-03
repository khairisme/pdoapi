using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/ruj-skala-kompetensi")]
    public class RujSkalaKompetensiExtController : ControllerBase
    {
        private readonly ILogger<RujSkalaKompetensiExtController> _logger;
        private readonly IRujSkalaKompetensiExt _rujskalakompetensiext;

        public RujSkalaKompetensiExtController(IRujSkalaKompetensiExt rujskalakompetensiext, ILogger<RujSkalaKompetensiExtController> logger)
        {
            _rujskalakompetensiext = rujskalakompetensiext;
            _logger = logger;
        }

    }
}
