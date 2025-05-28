namespace argumentParser.Classes.Actions;

public static class Fibonacci
{
    public static void Run()
    {
        Console.WriteLine(NthFibonacci(5));
    }
    public static int NthFibonacci(int n, int a = 0, int b = 1)
    {
        int c = a + b;
        if (n-- > 0)
            return NthFibonacci(n, b, c);
        else
            return c;
    }
}