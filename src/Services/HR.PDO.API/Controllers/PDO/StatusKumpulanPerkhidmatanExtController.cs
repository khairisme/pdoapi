using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/status-kumpulan-perkhidmatan")]
    public class StatusKumpulanPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<StatusKumpulanPerkhidmatanExtController> _logger;
        private readonly IStatusKumpulanPerkhidmatanExt _statuskumpulanperkhidmatanext;

        public StatusKumpulanPerkhidmatanExtController(IStatusKumpulanPerkhidmatanExt statuskumpulanperkhidmatanext, ILogger<StatusKumpulanPerkhidmatanExtController> logger)
        {
            _statuskumpulanperkhidmatanext = statuskumpulanperkhidmatanext;
            _logger = logger;
        }

    }
}
