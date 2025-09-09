using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/kategori-aktiviti-organisasi")]
    public class RujukanKategoriAktivitiOrganisasiExternalController : ControllerBase
    {
        private readonly ILogger<RujukanKategoriAktivitiOrganisasiExternalController> _logger;
        private readonly IRujKategoriAktivitiOrganisasiExt _rujkategoriaktivitiorganisasiext;

        public RujukanKategoriAktivitiOrganisasiExternalController(IRujKategoriAktivitiOrganisasiExt rujkategoriaktivitiorganisasiext, ILogger<RujukanKategoriAktivitiOrganisasiExternalController> logger)
        {
            _rujkategoriaktivitiorganisasiext = rujkategoriaktivitiorganisasiext;
            _logger = logger;
        }

    }
}
