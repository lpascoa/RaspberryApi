using System;
using System.Device.Gpio;
using Raspberry.API.Controllers;

namespace Raspberry.API
{
	public class ComedouroSevices
	{
        private readonly ILogger<ComedouroSevices> _logger;
        private GpioController _controller;
        private int pinEnvio = 26;
        private int pinRetorno = 24;
        private int lightTime = 1000;

        public ComedouroSevices(ILogger<ComedouroSevices> log)
        {
            _logger = log;
            DesligarPinos();
            _controller = new GpioController(PinNumberingScheme.Board);
        }

        public void AlimentarBruce(int tempo)
        {
            DesligarPinos();
            Thread.Sleep(lightTime * tempo);
            /*RecolherComida();
            Thread.Sleep(lightTime * 1);
            for (int i = 1; i <= tempo; i++)
            {
                DesligarPinos();
                EnviarComida();
                Thread.Sleep(lightTime * i);
                if (i % 2 == 0)
                {
                    DesligarPinos();
                    RecolherComida();
                    Thread.Sleep(lightTime * i);
                    DesligarPinos();
                }
            }*/
            DesligarPinos();
        }

        public void EnviarComida()
        {
            _controller.OpenPin(pinEnvio, PinMode.Output);
            _controller.Write(pinEnvio, PinValue.High);
            _logger.LogInformation($"Ligando o pin: {pinEnvio}");
        }

        public void RecolherComida()
        {
            _controller.OpenPin(pinRetorno, PinMode.Output);
            _controller.Write(pinRetorno, PinValue.High);
            _logger.LogInformation($"Ligando o pin: {pinRetorno}");
        }


        public void DesligarPinos()
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
    }
}

