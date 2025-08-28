using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/status-permohonan-skim-perkhidmatan")]
    public class StatusPermohonanSkimPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<StatusPermohonanSkimPerkhidmatanExtController> _logger;
        private readonly IStatusPermohonanSkimPerkhidmatanExt _statuspermohonanskimperkhidmatanext;

        public StatusPermohonanSkimPerkhidmatanExtController(IStatusPermohonanSkimPerkhidmatanExt statuspermohonanskimperkhidmatanext, ILogger<StatusPermohonanSkimPerkhidmatanExtController> logger)
        {
            _statuspermohonanskimperkhidmatanext = statuspermohonanskimperkhidmatanext;
            _logger = logger;
        }

    }
}
