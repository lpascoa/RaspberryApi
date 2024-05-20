using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Device.Gpio;
using System.Net.NetworkInformation;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Raspberry.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ComedouroController : ControllerBase
    {
        private readonly ILogger<ComedouroController> _logger;
        private ComedouroSevices _comedouro;

        public ComedouroController(ILogger<ComedouroController> log, ComedouroSevices comedouroSevices)
        {
            _logger = log;
            _comedouro = comedouroSevices;
            _comedouro.DesligarPinos();
        }

        [HttpPost("/enviarcomida/{tempo}")]
        public async Task<IActionResult> Enviarcomida([FromRoute] string tempo)
        {
            if (tempo.All(char.IsDigit))
            {
                _comedouro.AlimentarBruce(Convert.ToInt32(tempo));
                return Ok("Comida Enviada");
            }
            else
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { error = "Valor de tempo informado inválido" });
            }           
            
        }

        [HttpPost("/desligarEixo")]
        public async Task<IActionResult> DesligarEixo()
        {
            _comedouro.DesligarPinos();
            return Ok("Comedouro desligado");
        }
    }
}
