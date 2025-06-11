namespace argumentParser.Classes.Actions;

enum FibonacciMode
{
    NthElement,
    Sequence,
    AskUser
}

class Fibonacci : IActionable
{
    private FibonacciMode _mode = FibonacciMode.AskUser;
    private bool _clearOutput = false;
    private int _n = -1;

    public Fibonacci()
    {
        if (ArgumentParser.TryGet("mode", out Argument? modeArg) && modeArg!.IsArgument())
        {
            if (modeArg.GetValue() == "nthElement")
            {
                _mode = FibonacciMode.NthElement;
            }
            else if (modeArg.GetValue() == "sequence")
            {
                _mode = FibonacciMode.Sequence;
            }
        }

        if (ArgumentParser.TryGet("n", out Argument? nArg) && nArg!.IsArgument())
        {
            if (int.TryParse(nArg.GetValue(), out int n) && n >= 3)
            {
                _n = n;
            }
        }

        if (ArgumentParser.TryGet("clearOutput", out Argument? overwriteArg))
        {
            _clearOutput = true;
        }
    }

    public void Run()
    {
        string? userInput;

        if (_mode == FibonacciMode.AskUser)
        {
            while (true)
            {
                Console.Clear();
                Logger.DisplayMessage("Would you like to get nth-element of Fibonacci sequence or array of numbers?");
                Logger.DisplayMessage("1 - nth-element");
                Logger.DisplayMessage("2 - sequence");
                userInput = Console.ReadLine();
                int n;
                if (int.TryParse(userInput, out n) && (n == 1 || n == 2))
                {
                    if (n == 1) _mode = FibonacciMode.NthElement;
                    else if (n == 2) _mode = FibonacciMode.Sequence;
                    break;
                }
                Logger.DisplayMessage("Choose option 1 or 2. Press any key to continue...");
                Console.ReadKey();
            }
        }

        if (_n == -1)
        {
            switch (_mode)
            {
                case FibonacciMode.NthElement:
                    while (true)
                    {
                        Console.Clear();
                        Logger.DisplayMessage("Which (minimum 3rd) element of fibonacci sequence would you like to get? ");
                        userInput = Console.ReadLine();
                        int n;
                        if (int.TryParse(userInput, out n) && n >= 3)
                        {
                            _n = n;
                            break;
                        }
                        Logger.DisplayMessage("Please enter a valid greater than 2 integer. Press any key to continue...");
                        Console.ReadKey();
                    }
                    break;

                case FibonacciMode.Sequence:
                    while (true)
                    {
                        Console.Clear();
                        Logger.DisplayMessage("How many elements of fibonacci sequence would you like to get? ");
                        userInput = Console.ReadLine();
                        int n;
                        if (int.TryParse(userInput, out n) && n >= 0)
                        {
                            _n = n;
                            break;
                        }
                        Logger.DisplayMessage("Please enter a valid positive integer. Press any key to continue...");
                        Console.ReadKey();
                    }

                    break;
            }
        }

        switch (_mode)
        {
            case FibonacciMode.NthElement:
                if (_clearOutput)
                    Console.WriteLine(NthFibonacci(_n - 3));
                else
                    Logger.DisplayMessage($"The {_n}th element of the Fibonacci sequence is: {NthFibonacci(_n - 3)}");
                break;

            case FibonacciMode.Sequence:
                if (_clearOutput)
                    Console.WriteLine(string.Join(", ", FibonacciSequence(_n - 3)));

                else
                    Logger.DisplayMessage($"The first {_n} elements of the Fibonacci sequence are: {string.Join(", ", FibonacciSequence(_n - 3))}");
                break;
        }

    }

    public static int NthFibonacci(int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "n must be a non-negative integer.");
        if (n == 0) return 0;
        if (n == 1) return 1;

        int a = 0, b = 1, c = 0;
        for (int i = 2; i <= n; i++)
        {
            c = a + b;
            a = b;
            b = c;
        }
        return c;
    }

    public static List<int> FibonacciSequence(int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "n must be a non-negative integer.");
        List<int> sequence = new List<int> { 0, 1 };
        int a = 0, b = 1, c = 0;
        for (int i = 2; i <= n; i++)
        {
            c = a + b;
            a = b;
            b = c;
            sequence.Add(c);
        }
        return sequence;
    }

    public string GetName()
    {
        return "Fibonacci";
    }

    public string GetDescription()
    {
        string returnDescription =
            "Calculates the nth element of the Fibonacci sequence or returns the first n elements of the Fibonacci sequence.\n " +
            "If no arguments are provided, the user will be prompted to choose the mode and input the number." +
            "Example usage: -mode sequence -n 10\n" +
            "   optional: -clearOutput : outputs just the numbers without any description\n";

        return returnDescription;
    }
}