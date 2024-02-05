using Microsoft.AspNetCore.Mvc;

namespace API.NET.Controllers;

[ApiController]
//Se cambia la ruta URL por la cual se va a poder acceder al controlador
[Route("api/[controller]")]
public class HelloWorldController : ControllerBase
{
    //Se instancia la dependencia
    IHelloWorldService helloWorldService;

    //Se instancia el Logger
    private readonly ILogger<HelloWorldController> _logger;

    //Se debe de instanciar tambien dentro del constructor
    public HelloWorldController(IHelloWorldService helloWorld, ILogger<HelloWorldController> logger)
    {
        _logger = logger;
        helloWorldService = helloWorld;
    }

    [HttpGet]
    [Route("api/GetHelloWorld")]
    public IActionResult GetHelloWorld()
    {
        _logger.LogInformation("Se muestra el Hello World en pantalla");
        return Ok(helloWorldService.GetHelloWorld());
    }

    [HttpGet]
    [Route("api/GetRandom")]
    public IActionResult GetRandom()
    {
        _logger.LogInformation("Se muestra el numero random en pantalla");
        return Ok(helloWorldService.Random());
    }
}