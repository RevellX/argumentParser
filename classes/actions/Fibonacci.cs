namespace argumentParser.Classes.Actions;

enum FibonacciMode
{
    NthElement,
    Sequence,
    AskUser
}

class Fibonacci : IActionable
{
    private FibonacciMode _mode;
    private int _n;

    public Fibonacci()
    {
        _mode = FibonacciMode.AskUser;
        _n = 0;

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
        else
        {
            _mode = FibonacciMode.AskUser;
        }

        if (ArgumentParser.TryGet("n", out Argument? nArg) && nArg!.IsArgument())
        {
            if (int.TryParse(nArg.GetValue(), out int n))
            {
                _n = n;
            }
            else
            {
                _n = 0;
            }
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

        if (_n == 0)
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
                        if (int.TryParse(userInput, out n) && n >= 3)
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
                if (ArgumentParser.Has("clearOutput"))
                {
                    Console.WriteLine(NthFibonacci(_n - 3));
                }
                else
                {
                    Logger.DisplayMessage($"The {_n}th element of the Fibonacci sequence is: {NthFibonacci(_n - 3)}");
                }
                break;

            case FibonacciMode.Sequence:
                if (ArgumentParser.Has("clearOutput"))
                {
                    Console.WriteLine(FibonacciSequenceN(_n - 3));
                }
                else
                {
                    Logger.DisplayMessage($"The first {_n} elements of the Fibonacci sequence are: {FibonacciSequenceN(_n)}");
                }
                break;
        }

    }
    public static int NthFibonacci(int n, int a = 0, int b = 1)
    {
        int c = a + b;
        if (n-- > 0)
            return NthFibonacci(n, b, c);
        else
            return c;
    }

    public static int[] FibonacciSequenceN(int n)
    {
        return [];
    }

    public string GetName()
    {
        return "Fibonacci";
    }

    public string GetDescription()
    {
        string returnDescription =
            "Calculates the nth element of the Fibonacci sequence or returns the first n elements of the Fibonacci sequence.\n " +
            " Usage: -mode [nthElement|sequence] -n [number]\n" +
            "If no arguments are provided, the user will be prompted to choose the mode and input the number.";

        return returnDescription;
    }
}