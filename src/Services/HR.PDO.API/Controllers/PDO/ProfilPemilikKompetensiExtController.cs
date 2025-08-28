using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PPA;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/profil-pemilik-kompetensi")]
    public class ProfilPemilikKompetensiExtController : ControllerBase
    {
        private readonly ILogger<ProfilPemilikKompetensiExtController> _logger;
        private readonly IProfilPemilikKompetensiExt _profilpemilikkompetensiext;

        public ProfilPemilikKompetensiExtController(IProfilPemilikKompetensiExt profilpemilikkompetensiext, ILogger<ProfilPemilikKompetensiExtController> logger)
        {
            _profilpemilikkompetensiext = profilpemilikkompetensiext;
            _logger = logger;
        }

    }
}
