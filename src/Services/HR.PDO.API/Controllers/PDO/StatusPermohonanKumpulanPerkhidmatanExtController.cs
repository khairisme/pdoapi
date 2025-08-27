using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/status-permohonan-kumpulan-perkhidmatan")]
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
