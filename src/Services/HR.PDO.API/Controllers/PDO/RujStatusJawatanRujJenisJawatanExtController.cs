using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-status-jawatan-ruj-jenis-jawatan")]
    public class RujStatusJawatanRujJenisJawatanExtController : ControllerBase
    {
        private readonly ILogger<RujStatusJawatanRujJenisJawatanExtController> _logger;
        private readonly IRujStatusJawatanRujJenisJawatanExt _rujstatusjawatanrujjenisjawatanext;

        public RujStatusJawatanRujJenisJawatanExtController(IRujStatusJawatanRujJenisJawatanExt rujstatusjawatanrujjenisjawatanext, ILogger<RujStatusJawatanRujJenisJawatanExtController> logger)
        {
            _rujstatusjawatanrujjenisjawatanext = rujstatusjawatanrujjenisjawatanext;
            _logger = logger;
        }

    }
}
