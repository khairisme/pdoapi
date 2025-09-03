using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/status-permohonan-klasifikasi-perkhidmatan")]
    public class StatusPermohonanKlasifikasiPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<StatusPermohonanKlasifikasiPerkhidmatanExtController> _logger;
        private readonly IStatusPermohonanKlasifikasiPerkhidmatanExt _statuspermohonanklasifikasiperkhidmatanext;

        public StatusPermohonanKlasifikasiPerkhidmatanExtController(IStatusPermohonanKlasifikasiPerkhidmatanExt statuspermohonanklasifikasiperkhidmatanext, ILogger<StatusPermohonanKlasifikasiPerkhidmatanExtController> logger)
        {
            _statuspermohonanklasifikasiperkhidmatanext = statuspermohonanklasifikasiperkhidmatanext;
            _logger = logger;
        }

    }
}
