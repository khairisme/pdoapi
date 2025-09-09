using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/status-permohonan-jawatan")]
    public class RujukanStatusPermohonanJawatanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanStatusPermohonanJawatanExternalController> _logger;
        private readonly IRujStatusPermohonanJawatanExt _rujstatuspermohonanjawatanext;

        public RujukanStatusPermohonanJawatanExternalController(IRujStatusPermohonanJawatanExt rujstatuspermohonanjawatanext, ILogger<RujukanStatusPermohonanJawatanExternalController> logger)
        {
            _rujstatuspermohonanjawatanext = rujstatuspermohonanjawatanext;
            _logger = logger;
        }

    }
}
