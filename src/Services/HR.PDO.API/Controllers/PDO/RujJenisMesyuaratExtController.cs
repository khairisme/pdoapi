using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/ruj-jenis-mesyuarat")]
    public class RujJenisMesyuaratExtController : ControllerBase
    {
        private readonly ILogger<RujJenisMesyuaratExtController> _logger;
        private readonly IRujJenisMesyuaratExt _rujjenismesyuaratext;

        public RujJenisMesyuaratExtController(IRujJenisMesyuaratExt rujjenismesyuaratext, ILogger<RujJenisMesyuaratExtController> logger)
        {
            _rujjenismesyuaratext = rujjenismesyuaratext;
            _logger = logger;
        }

    }
}
