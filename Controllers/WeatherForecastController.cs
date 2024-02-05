using Microsoft.AspNetCore.Mvc;

namespace API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;


    //Se crea una lista estatica para poder manejar la informacion que se cree con el metodo GET
    private static List<WeatherForecast> ListWeatherForecast = new List<WeatherForecast>(); //Se inicializa la lista inmediatamente
    //La lista creada ya se puede instanciar con el nombre ListWeatherForecast

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;

        //Se transfiere el metodo para crear la lista dentro del constructor para que se mantenga en memoria y sea editable
        //Se evalua que a lista no sea nula o que no contenga ningun valor
        if(ListWeatherForecast == null || !ListWeatherForecast.Any()){
            //Se pasa con la creación de las listas 
            ListWeatherForecast = Enumerable.Range(1, 3).Select(index => new WeatherForecast{
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToList(); //Se deben de convertir los valores a una lista haciendo la conversion explicita
            //LOS DATOS QUE SE GENERAN SON TOTALMENTE RANDOM
        }
    }

    //Se inicia con la creación de los metodos HTTP
    //--------------------------------------------------------------------------------------------------------------------------------------------

    //Las rutas de las URL se pueden cambiar, afectando el acceso al metodo HTTP
    //Metodo GET
    [HttpGet(Name = "GetWeatherForecast")]
    //[Route("Get/weatherForecast")]
    //[Route("[action]")]
    public IEnumerable<WeatherForecast> Get()
    {
        //Se elimina el metodo para crear la lista random ya que siempre estaba creando una nueva lista cada vez que se hacia una solicitud HTTP
        // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        // {
        //     Date = DateTime.Now.AddDays(index),
        //     TemperatureC = Random.Shared.Next(-20, 55),
        //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        // })
        // .ToArray();

        //El método GET se encarga de retornar los datos, por lo que solo se hace un return de la lista creada

        //Uso del Logger
        _logger.LogInformation("Se muestra la lista del WeatherForecast");
        return ListWeatherForecast;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------
    //Metodo POST
    [HttpPost]
    public IActionResult Post(WeatherForecast weatherForecast){ //Va a recibir un parametro de tipo objeto WeatherForecast

        //Se añade un nuevo registro del objeto WeatherForecast a la lista creada
        ListWeatherForecast.Add(weatherForecast);
        //Se deberia de hacer los controles a posibles errores
        return Ok("Success");
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------
    //Metodo Delete
    //Se le debe de pasar el parametro index dentro del HTTPDELETE para que sea capaz de recibir el parametro en la URL
    [HttpDelete("{index}")]
    public IActionResult Delete(int index){
        if(ListWeatherForecast.Count <= index){
            return BadRequest("The Given ID is out of bonds");
        }
        ListWeatherForecast.RemoveAt(index);

        return Ok("Success");
    }
}
