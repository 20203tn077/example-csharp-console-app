namespace ExampleCSharpConsoleApp;

public static class ConsoleUtils
{
    private const int ConsoleWidth = 119;
    private const int SleepDelay = 1000;
    private const bool DefaultClean = false;
    private const ConsoleKey ConfirmationKey = ConsoleKey.Y;
    private const ConsoleKey RejectionKey = ConsoleKey.N;
    private const string StartMessage = "Presiona cualquier tecla para continuar...";
    private const string OptionSelectionMessage = "Selecciona una opción";
    private const string EmptyInputMessage = "Entrada inválida, debes escribir algo";
    private const string InvalidIntMessage = "Entrada inválida, debes ingresar un número entero";
    private const string InvalidDoubleMessage = "Entrada inválida, debes ingresar un número decimal";
    private const string InvalidOptionMessage = "Opción inválida";

    public static void Greeting(string title, string summary)
    {
        var bar = String.Concat(Enumerable.Repeat('=', ConsoleWidth));
        if (DefaultClean) Console.Clear();
        Console.WriteLine(
            $"""
            {bar}
            {AddPadding(title)}
            {bar}
            
            {summary}

            {StartMessage}

            """
        );
        Console.ReadKey(true);
        if (DefaultClean) Console.Clear();
    }

    public static void Farewell(string message)
    {
        if (DefaultClean) Console.Clear();
        Console.WriteLine(AddPadding(message));
        Thread.Sleep(SleepDelay);
    }

    public static string ReadString(string message)
    {
        var response = "";
        bool validInput;

        do
        {
            var input = InnerReadString(message);
            validInput = input != null;
            if (validInput) response = input ?? "";
            else Warning(EmptyInputMessage);
        } while (!validInput);

        return response;
    }

    public static int ReadInt(string message)
    {
        var response = 0;
        bool validInput;

        do
        {
            var input = InnerReadInt(message);
            validInput = input != null;
            if (validInput) response = input ?? 0;
            else Warning(InvalidIntMessage);
        } while (!validInput);

        return response;
    }

    public static double ReadDouble(string message)
    {
        var response = 0.0;
        bool validInput;

        do
        {
            var input = InnerReadDouble(message);
            validInput = input != null;
            if (validInput) response = input ?? 0;
            else Warning(InvalidDoubleMessage);
        } while (!validInput);

        return response;
    }

    public static T ReadOption<T>(string message, T[] options)
    {
        message += ":\n";
        for (int i = 0; i < options.Length; i++) message += $"{i + 1}.- {options[i]}\n";
        message += $"\n{OptionSelectionMessage}";
        bool validInput;
        var response = options[0];

        do
        {
            var input = InnerReadInt(message) ?? 0;
            validInput = (input >= 1 && input <= options.Length);
            if (validInput) response = options[input - 1];
            else Warning(InvalidOptionMessage);
        } while (!validInput);

        return response;
    }

    public static bool Confirm(string message)
    {
        var response = false;
        bool validInput;

        do
        {
            Print($"{message} [{ConfirmationKey}/{RejectionKey}]");
            var input = Console.ReadKey(true);
            validInput = input.Key is ConfirmationKey or RejectionKey;
            if (validInput) response = input.Key == ConfirmationKey;
            else Warning(InvalidOptionMessage);
        } while (!validInput);

        return response;
    }

    public static void Info(string message) => Info(message, DefaultClean);

    public static void Warning(string message) => Warning(message, DefaultClean);

    public static void Info(string message, bool clearAfter) => Alert(AddPadding(message), clearAfter);

    public static void Warning(string message, bool clearAfter) => Alert(AddPadding($"[!] {message} [!]"), clearAfter);

    public static void Alert(string message)
    {
        Alert(message, DefaultClean);
    }

    public static void Alert(string message, bool clearAfter)
    {
        Print(message);
        Thread.Sleep(SleepDelay);
        if (clearAfter) Console.Clear();
    }

    private static string? InnerReadString(string message)
    {
        Console.WriteLine($"{message}:");
        var value = InnerReadLine();
        if (!string.IsNullOrWhiteSpace(value)) return value;
        return null;
    }

    private static int? InnerReadInt(string message)
    {
        Console.WriteLine($"{message}:");
        if (int.TryParse(InnerReadLine(), out var value)) return value;
        return null;
    }

    private static double? InnerReadDouble(string message)
    {
        Console.WriteLine($"{message}:");
        if (double.TryParse(InnerReadLine(), out var value)) return value;
        return null;
    }

    private static string? InnerReadLine()
    {
        var input = Console.ReadLine();
        Console.WriteLine();
        return input;
    }

    public static void Print(string message) => Console.WriteLine($"{message}\n");

    private static string AddPadding(string message)
    {
        const char padChar = '-';
        message = $" {message} ";
        var spaceEnd = (ConsoleWidth - message.Length) / 2;
        return message.PadLeft(ConsoleWidth - spaceEnd, padChar).PadRight(ConsoleWidth, padChar);
    }
}