using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/implikasi-permohonan-jawatan")]
    public class ImplikasiPermohonanJawatanExtController : ControllerBase
    {
        private readonly ILogger<ImplikasiPermohonanJawatanExtController> _logger;
        private readonly IImplikasiPermohonanJawatanExt _implikasipermohonanjawatanext;

        public ImplikasiPermohonanJawatanExtController(IImplikasiPermohonanJawatanExt implikasipermohonanjawatanext, ILogger<ImplikasiPermohonanJawatanExtController> logger)
        {
            _implikasipermohonanjawatanext = implikasipermohonanjawatanext;
            _logger = logger;
        }

    }
}
