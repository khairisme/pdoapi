using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/status-permohonan-kumpulan-perkhidmatan")]
    public class StatusPermohonanKumpulanPerkhidmatanExtController : ControllerBase
    {
        private readonly ILogger<StatusPermohonanKumpulanPerkhidmatanExtController> _logger;
        private readonly IStatusPermohonanKumpulanPerkhidmatanExt _statuspermohonankumpulanperkhidmatanext;

        public StatusPermohonanKumpulanPerkhidmatanExtController(IStatusPermohonanKumpulanPerkhidmatanExt statuspermohonankumpulanperkhidmatanext, ILogger<StatusPermohonanKumpulanPerkhidmatanExtController> logger)
        {
            _statuspermohonankumpulanperkhidmatanext = statuspermohonankumpulanperkhidmatanext;
            _logger = logger;
        }

    }
}
