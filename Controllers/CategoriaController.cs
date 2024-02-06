using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using webapi.Models;


namespace webapi.Controllers
{
    //Siempre se debe de heredar el ControllerBase para los controladores
    [ApiController]
    [Route("apiCategoria/[controller]")]
    public class CategoriaController : ControllerBase
    {
        //Se usa la interfaz de la categoria en vez de la clase
        ICategoriaService categoriaService;

        public CategoriaController(ICategoriaService service)
        {
            categoriaService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(categoriaService.GetCategorias());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Categoria categoria)
        {
            categoriaService.SaveCategoria(categoria);
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult Put(Guid id, [FromBody] Categoria categoria)
        {
            categoriaService.UpdateCategoria(id, categoria);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            categoriaService.DeleteCategoria(id);
            return Ok();
        }
    }
}