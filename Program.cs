using webapi;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//----------------------------------------------------------------------------------------------------------------------------------------------
//SE DEBE DE AGREGAR EL SERVICIO DE ENTITY FRAMEWORK AL PROGRAM
//EL builder SE ENCARGA DE OBTENER EL STRING DE CONEXION PARA LA BASE DE DATOS QUE SE DEFINIO EN EL APPSETTINGS
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("conectionTareas"));

//Se inyecta la dependencia en la clase program
builder.Services.AddScoped<IHelloWorldService,HelloWorldService>();
//Otro forma por la cual se puede agregar una dependencia es referenciando toda la clase, sin embargo no es tan recomendable
//builder.Services.AddScoped<IHelloWorldService>(p => new HelloWorldService());

//----------------------------------------------------------------------------------------------------------------------------------------------
//SE INYECTAN LAS DEPENDENCIAS AL PROGRAM
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ITareaService, TareaService>();
//----------------------------------------------------------------------------------------------------------------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Se agrega el Middleware creado despues del Middleware de autorización ya que ahi es donde se agregan los Middlewares personalizados
//app.UseTimeMiddleware();

app.MapControllers();

app.Run();
