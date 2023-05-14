namespace ExampleCSharpConsoleApp;

public enum Constraint
{
    Positive,
    PositiveOrZero,
    Negative,
    NegativeOrZero
};

public static class ConsoleUtils
{
    private const int ConsoleWidth = 119; 
    private const int SleepDelay = 1500; 
    private const bool DefaultClear = false;
    private const ConsoleKey ConfirmationKey = ConsoleKey.S; 
    private const ConsoleKey RejectionKey = ConsoleKey.N; 
    private const string StartMessage = "Presiona cualquier tecla para continuar..."; 
    private const string OptionSelectionMessage = "Selecciona una opción"; 
    private const string EmptyInputMessage = "Entrada inválida, debes escribir algo"; 
    private const string InvalidIntMessage = "Entrada inválida, debes ingresar un número entero"; 
    private const string InvalidDoubleMessage = "Entrada inválida, debes ingresar un número decimal"; 
    private const string InvalidOptionMessage = "Opción inválida"; 
    private const string MustBePositiveMessage = "Debes ingresar un número mayor a cero"; 
    private const string MustBePositiveOrZeroMessage = "Debes ingresar un número mayor o igual a cero"; 
    private const string MustBeNegativeMessage = "Debes ingresar un número menor a cero"; 
    private const string MustBeNegativeOrZeroMessage = "Debes ingresar un número menor o igual a cero"; 

    public static void Greeting(string title, string summary)
    {
        var bar = String.Concat(Enumerable.Repeat('=', ConsoleWidth));
        if (DefaultClear) ClearIfDefault();
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
        if (DefaultClear) ClearIfDefault();
    }

    public static void Farewell(string message)
    {
        if (DefaultClear) ClearIfDefault();
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

    public static int ReadInt(string message, Constraint? constraint)
    {
        var response = 0;
        bool validInput;
        bool constraintViolation = false;

        do
        {
            var input = InnerReadInt(message);
            validInput = input != null;
            if (validInput)
            {
                constraintViolation = !CompliesConstraint(input ?? 0, constraint);
                if (!constraintViolation) response = input ?? 0;
                else Warning(GetConstraintMessage(constraint));
            }
            else Warning(InvalidIntMessage);
        } while (!validInput || constraintViolation);

        return response;
    }

    public static double ReadDouble(string message, Constraint? constraint)
    {
        var response = 0.0;
        bool validInput;
        bool constraintViolation = false;

        do
        {
            var input = InnerReadDouble(message);
            validInput = input != null;
            if (validInput)
            {
                constraintViolation = !CompliesConstraint(input ?? 0, constraint);
                if (!constraintViolation) response = input ?? 0;
                else Warning(GetConstraintMessage(constraint));
            }
            else Warning(InvalidDoubleMessage);
        } while (!validInput || constraintViolation);

        return response;
    }

    public static T ReadOption<T>(string message, T[] options)
    {
        message += "\n";
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

    public static void Info(string message) => Info(message, DefaultClear);

    public static void Warning(string message) => Warning(message, DefaultClear);

    public static void Info(string message, bool clearAfter) => Alert(AddPadding(message), clearAfter);

    public static void Warning(string message, bool clearAfter) => Alert(AddPadding($"[!] {message} [!]"), clearAfter);

    public static void Alert(string message)
    {
        Alert(message, DefaultClear);
    }

    public static void Alert(string message, bool clearAfter)
    {
        Print(message);
        Thread.Sleep(SleepDelay);
        if (clearAfter) ClearIfDefault();
    }

    public static void ClearIfDefault()
    {
        if (DefaultClear) Console.Clear();
    }

    private static string GetConstraintMessage(Constraint? constraint) => constraint switch
    {
        Constraint.Positive => MustBePositiveMessage,
        Constraint.PositiveOrZero => MustBePositiveOrZeroMessage,
        Constraint.Negative => MustBeNegativeMessage,
        Constraint.NegativeOrZero => MustBeNegativeOrZeroMessage,
        _ => ""
    };

    private static bool CompliesConstraint(double value, Constraint? constraint) => constraint switch
    {
        Constraint.Positive => value > 0,
        Constraint.PositiveOrZero => value >= 0,
        Constraint.Negative => value < 0,
        Constraint.NegativeOrZero => value <= 0,
        null => true,
        _ => false
    };

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