using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-kategori-aktiviti-organisasi")]
    public class RujKategoriAktivitiOrganisasiExtController : ControllerBase
    {
        private readonly ILogger<RujKategoriAktivitiOrganisasiExtController> _logger;
        private readonly IRujKategoriAktivitiOrganisasiExt _rujkategoriaktivitiorganisasiext;

        public RujKategoriAktivitiOrganisasiExtController(IRujKategoriAktivitiOrganisasiExt rujkategoriaktivitiorganisasiext, ILogger<RujKategoriAktivitiOrganisasiExtController> logger)
        {
            _rujkategoriaktivitiorganisasiext = rujkategoriaktivitiorganisasiext;
            _logger = logger;
        }

    }
}
