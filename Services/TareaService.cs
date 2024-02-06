using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

//Se cambia el namespace para que coincida con el de las demas clases
namespace webapi.Services
{
    public class TareaService: ITareaService
    {
        private readonly ILogger<TareaService> _logger;

        //Se hace referencia al contexto de Entity Framework
        TareasContext context;

        //Se crea el constructor de la clase haciendo referencia al contexto de la base de datos
        public TareaService(TareasContext dbContext, ILogger<TareaService> logger)
        {
            _logger = logger;
            context = dbContext;
        }

        //Metodo GET para mostrar la información en pantalla
        public IEnumerable<Tarea> GetTareas()
        {
            return context.Tareas;
        }

        //Metodo SAVE para guardar la informacion de una nueva Tarea
        public async Task SaveTarea(Tarea tarea)
        {
            context.Add(tarea);
            await context.SaveChangesAsync();
            Results.Ok("Registro Guardado");
        }

        //Metodo UPDATE para actualizar la informacion de un registro
        public async Task UpdateTarea(Guid id, Tarea tarea)
        {
            var tareaActual = context.Tareas.Find(id);

            if(tareaActual != null)
            {
                tareaActual.CategoriaId = tarea.CategoriaId;
                tareaActual.Titulo = tarea.Titulo;
                tareaActual.Descripcion = tarea.Descripcion;
                tareaActual.PrioridadTarea = tarea.PrioridadTarea;
                tareaActual.FechaCreacion = tarea.FechaCreacion;
                await context.SaveChangesAsync();
            }
            _logger.LogInformation("No se pudo completar la acción");
        }

        //Metodo DELETE para eliminar un registro
        public async Task DeleteTarea(Guid id)
        {
            var tareaActual = context.Tareas.Find(id);

            if(tareaActual != null)
            {
                context.Remove(tareaActual);
                await context.SaveChangesAsync();
            }

            _logger.LogInformation("No se pudo completar la acción");
        }
    }

    //Se crea la interfaz
    public interface ITareaService
    {
        IEnumerable<Tarea> GetTareas();
        Task SaveTarea(Tarea tarea);
        Task UpdateTarea(Guid id, Tarea tarea);
        Task DeleteTarea(Guid id);
    }
}