using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;

//Se debe de cambiar el nameespace para que coincida con el de TareasContext
namespace webapi.Services
{
    public class CategoriaService : ICategoriaService
    {

        private readonly ILogger<CategoriaService> _logger;
        //----------------------------------------------------------------------------------------------------------------------------------------------
        //SE INICIA CREANDO TODA LA INFORMACIÓN PARA EL MODELO DE CATEGORIA 
        //----------------------------------------------------------------------------------------------------------------------------------------------
        //Se hace la instancia de la clase que contiene todo el contexto de la base de datos de Entity Framework
        TareasContext context;

        //Se crea el constructor de la clase haciendo referencia a contexto de la base de datos
        public CategoriaService(TareasContext dbContext, ILogger<CategoriaService> logger)
        {
            _logger=logger;
            context = dbContext;
        }

        //Metodo GET para mostrar la informacion en la pantalla
        public IEnumerable<Categoria> GetCategorias()
        {
            return context.Categorias;
        }

        //Metodo SAVE para guardar la información
        public async Task SaveCategoria(Categoria categoria)
        {
            context.Add(categoria);
            await context.SaveChangesAsync();
        }

        //Metodo UPDATE para actualizar la informacion de un registro
        public async Task UpdateCategoria(Guid id, Categoria categoria)
        {
            var categoriaActual = context.Categorias.Find(id);

            if(categoriaActual == null)
            {
                _logger.LogInformation("No se pudo completar la accion");
            }
            
            categoriaActual.Nombre = categoria.Nombre;
            categoriaActual.Descripcion=categoria.Descripcion;
            categoriaActual.Peso=categoria.Peso;

            await context.SaveChangesAsync();
        }

        //Metodo DELETE para eliminar un registro
        public async Task DeleteCategoria(Guid id)
        {
            var categoriaActual = context.Categorias.Find(id);

            if(categoriaActual != null)
            {
                context.Remove(categoriaActual);
                await context.SaveChangesAsync();
            }
        }
    }

    //Se define la interfaz que solo mostrará la informacion necesaria a las pantallas
    public interface ICategoriaService
    {
        IEnumerable<Categoria> GetCategorias();
        Task SaveCategoria(Categoria categoria);
        Task UpdateCategoria(Guid id, Categoria categoria);
        Task DeleteCategoria(Guid id);
    }
}