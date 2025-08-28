using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PPA;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-negara")]
    public class RujNegaraExtController : ControllerBase
    {
        private readonly ILogger<RujNegaraExtController> _logger;
        private readonly IRujNegaraExt _rujnegaraext;

        public RujNegaraExtController(IRujNegaraExt rujnegaraext, ILogger<RujNegaraExtController> logger)
        {
            _rujnegaraext = rujnegaraext;
            _logger = logger;
        }

    }
}
