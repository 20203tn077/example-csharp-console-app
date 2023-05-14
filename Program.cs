namespace ExampleCSharpConsoleApp;

public class ExampleApp
{
    private const string Title = "EJEMPLO DE APLICACIÓN EN CONSOLA";

    private const string Summary = (
        """
        Este es un ejemplo de aplicación en consola hecho con C#.
        En esta aplicación, podrás probar algunas de las funciones de utilidades de consola que tiene la clase ConsoleUtils. Las utilidades abarcan principalmente validación de datos, pero se incluyen también algunas herramientas de despliegue de información en pantalla. 
        """
    );

    private static readonly string[] Strings = { "Opción 1", "Opción 2", "Opción 3", "Opción 4", "Opción 5" };
    private static readonly int[] Ints = { 2, 3, 5, 7, 11, 13, 17 };
    private static readonly double[] Doubles = { -23.0, 0.1, 7.9 };

    public static void Main()
    {
        ConsoleUtils.Greeting(Title, Summary);
        
        do
        {
            ConsoleUtils.Print("Lectura de texto");
            var stringInput = ConsoleUtils.ReadString("Ingresa algo de texto");
            ConsoleUtils.Alert($"Texto ingresado: {stringInput}");
            
            ConsoleUtils.Print("Lectura de números enteros");
            var intInput = ConsoleUtils.ReadInt("Ingresa un número entero");
            ConsoleUtils.Alert($"Número ingresado: {intInput}");
            
            ConsoleUtils.Print("Lectura de números decimales");
            var doubleInput = ConsoleUtils.ReadDouble("Ingresa un número decimal");
            ConsoleUtils.Alert($"Número ingresado: {doubleInput}");
            
            ConsoleUtils.Print("Lectura de texto");
            var stringOption = ConsoleUtils.ReadOption("Opciones de texto", Strings);
            ConsoleUtils.Alert($"Texto seleccionado: {stringOption}");
            
            ConsoleUtils.Print("Lectura de números enteros");
            var intOption = ConsoleUtils.ReadOption("Opciones de números enteros", Ints);
            ConsoleUtils.Alert($"Número seleccionado: {intOption}");
            
            ConsoleUtils.Print("Lectura de números decimales");
            var doubleOption = ConsoleUtils.ReadOption("Opciones de números decimales", Doubles);
            ConsoleUtils.Alert($"Número seleccionado: {doubleOption}");
            
            ConsoleUtils.Print("Confirmación de acciones");
        } while (!ConsoleUtils.Confirm("¿Deseas salir de la aplicación?"));

        ConsoleUtils.Farewell("¡Adiós!");
    }
}