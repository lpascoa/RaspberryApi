using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Device.Gpio;
using System.Net.NetworkInformation;
using System.Threading;

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
        public bool Enviarcomida([FromRoute]int tempo)
        {
            _comedouro.AlimentarBruce(tempo);
            return true;
        }

        [HttpPost("/desligarEixo")]
        public bool DesligarEixo()
        {
            _comedouro.DesligarPinos();
            return true;
        }
    }
}
