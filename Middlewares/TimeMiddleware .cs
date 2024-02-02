using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.NET.Middlewares;

namespace API.NET.Middlewares
{
    public class TimeMiddleware 
    {
        //Se crea una propiedad ReadOnly que se va a usar para pasar al siguiente Middleware
        //El RequestDelegate es la propiedad que ayuda a hacer el salto hacia el otro middleware
        readonly RequestDelegate next;

        //Se crea el constructor para la clase
        public TimeMiddleware(RequestDelegate nextRequest)
        {
            next = nextRequest;
        }

        //Se crea una tarea asincrona de nombre Invoke
        //El metodo HttpContent contiene todo el contexto o la informacion del request que se está realizando
        public async Task Invoke(HttpContext context)
        {
            await next(context);

            if(context.Request.Query.Any(p=>p.Key == "time"))
            {
                await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
                return;
            }

            //await next(context);
            //Si se crea el await despues de realizar la ejecucion, el middleware cambiará toda la estructura de la aplicación
        }
    }
}


//Se crea la extencion del Middleware para que se ejecute
public static class TimeMiddlewareExtension
{
    //El metodo IApplicationBuilder se usa para permitirle interactuar con la aplicacion
    //La clase es de tipo IApplicationBuilder por lo que retorna ese parametro, pero recibe el mismo parametro como argumento
    public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder builder)
    {
        //Finalmente retorna el IApplicationBuilder con los cambios del MiddleWare ya realizados
        return builder.UseMiddleware<TimeMiddleware>();
    }
}