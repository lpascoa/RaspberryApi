# RaspberryApi
A lightweight **.NET 8 Web API prototype** built to experiment with controlling hardware connected to a **Raspberry Pi** through GPIO pins.
The project was created as a hands-on test to expose HTTP endpoints that could trigger commands on a motorized mechanism connected to the Raspberry Pi. In my original setup, the API was used to test movement commands for a servo/motor mechanism used in a small feeder prototype.
## Why I Built It
The goal was to validate a simple idea end-to-end:
**Can I send an HTTP request to a Raspberry Pi and use it to control physical hardware through GPIO?**
Instead of only testing GPIO directly on the device, I wrapped the hardware interaction behind a REST API. This made it possible to trigger and test hardware behavior remotely and gave me a simple foundation for experimenting with IoT-style integrations.
## What It Does
The API exposes endpoints that execute hardware commands on the Raspberry Pi.
The current prototype can:
- Receive HTTP commands through an ASP.NET Core API.
- Control Raspberry Pi GPIO output pins using `System.Device.Gpio`.
- Trigger forward/return actions on the connected motor mechanism.
- Execute timed movement sequences.
- Turn the controlled GPIO outputs off after an operation.
- Expose Swagger/OpenAPI for testing the endpoints.
- Expose a health-check endpoint.
- Log API requests and hardware operations with Serilog.
- Run as a Linux container using Docker.
## Architecture

```text\nHTTP Client
|
v
ASP.NET Core Web API
|
v
ComedouroController
|
v
ComedouroSevices
|
v
System.Device.Gpio
|
v
Raspberry Pi GPIO
|
v
Motor / Servo Mechanism
```
This is intentionally a small prototype. The focus was validating the complete path from an HTTP request to a physical action on hardware.
## Technologies
- C#
- .NET 8
- ASP.NET Core Web API
- Raspberry Pi
- `System.Device.Gpio`
- REST APIs
- Swagger / OpenAPI
- Serilog
- ASP.NET Core Health Checks
- Docker
- Linux
## API Endpoints
### Trigger the motor sequence
```http
POST /enviarcomida/{tempo}
```
`tempo` controls how many iterations are used in the timed motor sequence.
Example:
```http
POST /enviarcomida/2
```
### Turn the GPIO outputs off
```http
POST /desligarEixo
```
### Health check
```http
GET /healthz
```
## Running the API
The application targets **.NET 8** and listens on port **9090**.
```bash
dotnet restore
dotnet run
```
Swagger is available after startup through the application's Swagger UI.
Because the hardware layer uses Raspberry Pi GPIO, the hardware-control functionality is intended to run on a compatible Raspberry Pi/Linux environment with the required GPIO access.
## Docker
The repository includes a `Dockerfile` so the API can be packaged as a Linux container.
```bash
docker build -t raspberry-api .
docker run --device /dev/gpiomem -p 9090:9090 raspberry-api
```
> GPIO device mapping may vary depending on the Raspberry Pi OS, container runtime, and hardware configuration.
## What I Learned
This project was a small R&D exercise rather than a production application. It gave me a practical way to experiment with:
- Bridging web software and physical hardware.
- Controlling GPIO from C#/.NET.
- Exposing hardware operations through an API.
- Testing device behavior remotely.
- Containerizing a hardware-adjacent .NET application.
- Turning a simple technical question into a working prototype quickly.
## Possible Next Steps\
If I continue evolving the prototype, useful additions would include:
- Non-blocking/background execution for motor commands.
- Authentication and authorization for remote commands.
- Configurable GPIO mappings instead of hard-coded pins.
- Better hardware-state reporting and telemetry.
- MQTT or another lightweight IoT messaging protocol.
- Sensor feedback to validate physical movement.
- Automated tests around the API and hardware abstraction layer.
- Integration with an edge/IoT platform for remote device management.
---
This repository represents a small, hands-on prototype focused on answering a practical question with working software and real hardware.
