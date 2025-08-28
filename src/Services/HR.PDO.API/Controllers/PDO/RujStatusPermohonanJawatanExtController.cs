using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-status-permohonan-jawatan")]
    public class RujStatusPermohonanJawatanExtController : ControllerBase
    {
        private readonly ILogger<RujStatusPermohonanJawatanExtController> _logger;
        private readonly IRujStatusPermohonanJawatanExt _rujstatuspermohonanjawatanext;

        public RujStatusPermohonanJawatanExtController(IRujStatusPermohonanJawatanExt rujstatuspermohonanjawatanext, ILogger<RujStatusPermohonanJawatanExtController> logger)
        {
            _rujstatuspermohonanjawatanext = rujstatuspermohonanjawatanext;
            _logger = logger;
        }

    }
}
