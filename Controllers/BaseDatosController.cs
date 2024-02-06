using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("apiDB/[controller]")]
    public class BaseDatosController : ControllerBase
    {
        //Se crea una clase con el proposito de manejar la creación de la base de datos

        //Se hace referencia al context de EntityFramework para manejar
        TareasContext dbContext;
        private readonly ILogger<BaseDatosController> _logger;
        
        public BaseDatosController(TareasContext context, ILogger<BaseDatosController> logger)
        {
            _logger = logger;
            dbContext = context;
        }

        //Se crea el metodo por el cual se va a crear la base de datos
        [HttpGet]
        public IActionResult createDatabase()
        {
            dbContext.Database.EnsureCreated();
            _logger.LogInformation("Base de datos creada");
            return Ok();
        }
    }
}