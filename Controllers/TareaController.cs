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
    [Route("apiTarea/[controller]")]
    public class TareaController : ControllerBase
    {
        ITareaService tareaService;

        public TareaController(ITareaService service)
        {
            tareaService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(tareaService.GetTareas());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Tarea tarea)
        {
            tareaService.SaveTarea(tarea);
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult Put(Guid id, [FromBody] Tarea tarea)
        {
            tareaService.UpdateTarea(id,tarea);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            tareaService.DeleteTarea(id);
            return Ok();
        }
    }
}