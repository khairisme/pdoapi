using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/jenis-mesyuarat")]
    public class RujukanJenisMesyuaratExternalController : ControllerBase
    {
        private readonly ILogger<RujukanJenisMesyuaratExternalController> _logger;
        private readonly IRujJenisMesyuaratExt _rujjenismesyuaratext;

        public RujukanJenisMesyuaratExternalController(IRujJenisMesyuaratExt rujjenismesyuaratext, ILogger<RujukanJenisMesyuaratExternalController> logger)
        {
            _rujjenismesyuaratext = rujjenismesyuaratext;
            _logger = logger;
        }

    }
}
