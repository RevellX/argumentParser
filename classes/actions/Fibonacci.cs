namespace argumentParser.Classes.Actions;

public static class Fibonacci
{
    public static void Run()
    {

        int n;
        string? userInput;
        while (true)
        {
            Console.Clear();
            Logger.DisplayMessage("Would you like to get nth-element of Fibonacci sequence or array of numbers?");
            Logger.DisplayMessage("1 - nth-element");
            Logger.DisplayMessage("2 - sequence");
            userInput = Console.ReadLine();
            if (int.TryParse(userInput, out n) && (n == 1 || n == 2))
                break;
            Logger.DisplayMessage("Choose option 1 or 2. Press any key to continue...");
            Console.ReadKey();
        }

        switch (n)
        {
            case 1:
                while (true)
                {
                    Console.Clear();
                    Logger.DisplayMessage("Which (minimum 3rd) element of fibonacci sequence would you like to get? ");
                    userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out n) && n >= 3)
                        break;
                    Logger.DisplayMessage("Please enter a valid greater than 2 integer. Press any key to continue...");
                    Console.ReadKey();
                }
                Console.WriteLine(NthFibonacci(n - 3));
                break;

            case 2:
                while (true)
                {
                    Console.Clear();
                    Logger.DisplayMessage("How many elements of fibonacci sequence would you like to get? ");
                    userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out n) && n >= 3)
                        break;
                    Logger.DisplayMessage("Please enter a valid positive integer. Press any key to continue...");
                    Console.ReadKey();
                }
                Console.WriteLine(FibonacciSequenceN(n));
                break;

            default:
                Logger.DisplayMessage(Messages.OPTION_UNKNOW, MesssageType.ERROR);
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
}