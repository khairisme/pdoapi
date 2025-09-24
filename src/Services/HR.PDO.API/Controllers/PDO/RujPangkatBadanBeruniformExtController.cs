using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.DTOs;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/rujukan/pangkat")]
    public class RujPangkatBadanBeruniformExtController : ControllerBase
    {
        private readonly ILogger<RujPangkatBadanBeruniformExtController> _logger;
        private readonly IRujPangkatBadanBeruniformExt _rujpangkatbadanberuniformext;
        private readonly IHttpClientFactory httpClientFactory;

        public RujPangkatBadanBeruniformExtController(IRujPangkatBadanBeruniformExt rujpangkatbadanberuniformext, ILogger<RujPangkatBadanBeruniformExtController> logger)
        {
            _rujpangkatbadanberuniformext = rujpangkatbadanberuniformext;
            _logger = logger;
        }

        [HttpGet("call-pangkat")]
        public async Task<IActionResult> CallPangkatApi([FromServices] IHttpClientFactory httpFactpry)
        {

            try
            {
                var data = await _rujpangkatbadanberuniformext.GetPangkatAsync();
                return Ok(new
                {
                    status = data.Count() > 0 ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in CallPangkatApi");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
            }
            //var client = httpClientFactory.CreateClient("PpaAPI");

            //var response = await client.GetAsync("/api/pdo/v1/rujukan/pangkat"); // appsettings already contains base url
            //if (!response.IsSuccessStatusCode)
            //{
            //    return StatusCode((int)response.StatusCode, "Pangkat API failed");
            //}

            //var options = new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true
            //};


            //var json = await response.Content.ReadAsStringAsync();
            //var data = JsonSerializer.Deserialize<List<DropDownDto>>(json, options);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>>RujukanPangkat()
        {
            _logger.LogInformation("Calling RujukanPangkat");
            try
            {
                var data = await _rujpangkatbadanberuniformext.RujukanPangkat();
                return Ok(new
                {
                    status = data.Count() > 0 ? "Berjaya" : "Gagal",
                    items = data

                });
            }
            catch (Exception ex)
            {
                 String err = "";
                 if (ex != null) { 
                     _logger.LogError(ex, "Error in RujukanPangkat");
                     if (ex.InnerException!=null) {
                         err = ex.InnerException.Message.ToString();
                     }
                }
                 
                return StatusCode(500, ex.Message+"-"+err);
            }
        }

    }
}
