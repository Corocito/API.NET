//Se debe de instanciar la interfaz en la clase para que las demás clases puedan acceder
public class HelloWorldService : IHelloWorldService
{
    //Se crea una clase básica que retorna un Hello World
    public string GetHelloWorld()
    {
        return "Hello World!";
    }

    public int Random()
    {
        return new Random().Next(10000);
    }
}

//Las interfaces solamente permiten el ver y manejar la información que se encuentre definida dentro de si. Por lo que las demás clases
//deben de hacer referencia a la interfaz en vez de la clase

public interface IHelloWorldService
{
    //Se instancia el metodo de Hello World en la interfaz para que las demás clases puedan acceder a ella
    string GetHelloWorld();
    int Random();
}

