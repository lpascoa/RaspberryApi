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
        private int pinEnvio = 26;
        private int pinRetorno = 24;

        public ComedouroController(ILogger<ComedouroController> log)
        {
            _logger = log;
            DesligarPinos();
        }

        [HttpPost("/enviarcomida/{tempo}")]
        public bool Enviarcomida([FromRoute]int tempo)
        {
            GpioController _controller = new GpioController(PinNumberingScheme.Board);
            var lightTime = 1000;
            _controller.OpenPin(pinEnvio, PinMode.Output);
            _controller.Write(pinEnvio, PinValue.High);
            _logger.LogInformation($"Ligando o pin: {pinEnvio}");
            Thread.Sleep(lightTime * tempo);
            _controller.Write(pinEnvio, PinValue.Low);
            _controller.ClosePin(pinEnvio);
            DesligarPinos();
            return true;
        }

        [HttpPost("/desligarEixo")]
        public bool DesligarEixo()
        {
            DesligarPinos();
            return true;
        }


        private void DesligarPinos()
        {
            GpioController _controller = new GpioController(PinNumberingScheme.Board);

            _logger.LogInformation($"Desligando o pin: {pinEnvio}");
            _controller.OpenPin(pinEnvio, PinMode.Output);
            _controller.Write(pinEnvio, PinValue.Low);
            _controller.ClosePin(pinEnvio);

            _logger.LogInformation($"Desligando o pin: {pinRetorno}");
            _controller.OpenPin(pinRetorno, PinMode.Output);
            _controller.Write(pinRetorno, PinValue.Low);
            _controller.ClosePin(pinRetorno);

        }
        // GET api/values/5
        //[HttpPost("{id}")]
        //public bool Get(uint id)
        //{
        //    var pin = GetPin(id);
        //    pin.PinMode = GpioPinDriveMode.Output;
        //    var isOn = pin.Read();

        //    pin.Write(!isOn);

        //    return !isOn;
        //}

        //private GpioPin GetPin(uint value)
        //{
        //    GpioPin pin;

        //    switch (value)
        //    {
        //        case 0:
        //            pin = Pi.Gpio.Pin00;
        //            break;
        //        case 1:
        //            pin = Pi.Gpio.Pin01;
        //            break;
        //        case 2:
        //            pin = Pi.Gpio.Pin02;
        //            break;
        //        case 3:
        //            pin = Pi.Gpio.Pin03;
        //            break;
        //        case 4:
        //            pin = Pi.Gpio.Pin04;
        //            break;
        //        case 5:
        //            pin = Pi.Gpio.Pin05;
        //            break;
        //        case 6:
        //            pin = Pi.Gpio.Pin06;
        //            break;
        //        case 7:
        //            pin = Pi.Gpio.Pin07;
        //            break;
        //        case 8:
        //            pin = Pi.Gpio.Pin08;
        //            break;
        //        case 9:
        //            pin = Pi.Gpio.Pin09;
        //            break;
        //        case 10:
        //            pin = Pi.Gpio.Pin10;
        //            break;
        //        case 11:
        //            pin = Pi.Gpio.Pin11;
        //            break;
        //        case 12:
        //            pin = Pi.Gpio.Pin12;
        //            break;
        //        case 13:
        //            pin = Pi.Gpio.Pin13;
        //            break;
        //        case 14:
        //            pin = Pi.Gpio.Pin14;
        //            break;
        //        case 15:
        //            pin = Pi.Gpio.Pin15;
        //            break;
        //        case 16:
        //            pin = Pi.Gpio.Pin16;
        //            break;
        //        case 17:
        //            pin = Pi.Gpio.Pin17;
        //            break;
        //        case 18:
        //            pin = Pi.Gpio.Pin18;
        //            break;
        //        case 19:
        //            pin = Pi.Gpio.Pin19;
        //            break;
        //        case 20:
        //            pin = Pi.Gpio.Pin20;
        //            break;
        //        case 21:
        //            pin = Pi.Gpio.Pin21;
        //            break;
        //        case 22:
        //            pin = Pi.Gpio.Pin22;
        //            break;
        //        case 23:
        //            pin = Pi.Gpio.Pin23;
        //            break;
        //        case 24:
        //            pin = Pi.Gpio.Pin24;
        //            break;
        //        case 25:
        //            pin = Pi.Gpio.Pin25;
        //            break;
        //        case 26:
        //            pin = Pi.Gpio.Pin26;
        //            break;
        //        case 27:
        //            pin = Pi.Gpio.Pin27;
        //            break;
        //        case 28:
        //            pin = Pi.Gpio.Pin28;
        //            break;
        //        case 29:
        //            pin = Pi.Gpio.Pin29;
        //            break;
        //        case 30:
        //            pin = Pi.Gpio.Pin30;
        //            break;
        //        case 31:
        //            pin = Pi.Gpio.Pin31;
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 31");
        //    }

        //    return pin;
        //}
    }
}
