using argumentParser.Classes;
using argumentParser.Classes.Actions;
using argumentParser.Classes.Utils;

namespace argumentParser;

public class Program
{
    public static void Main(string[] args)
    {
        ArgumentParser.Parse(args);
        // ArgumentParser.DisplayArgs();

        if (ArgumentParser.TryGet("action", out Argument? switchArg) && switchArg!.IsArgument())
        {
            switch (switchArg?.GetValue())
            {
                case "fibonacci":
                    new Fibonacci().Run();
                    break;
                case "multithreading":
                    new Multithreading().Run();
                    break;
                case "sorting":
                    new Sorting().Run();
                    break;
                case "test":
                    TestMethod();
                    break;
                default:
                    Logger.DisplayMessage(Messages.ACTION_UNKNOWN + $": {switchArg?.GetValue()}", MesssageType.ERROR);
                    break;
            }
        }
        else
        {
            Logger.DisplayMessage(Messages.ACTION_ARGUMENT_MISSING, MesssageType.ERROR);
        }
    }

    private static void TestMethod()
    {

        string filePath = "input.txt";
        var list = NumbersGenerator.GenerateRandomNumbers(1000000, 0, 100000);
        // Write the list to a file
        File.WriteAllLines(filePath, list.Select(x => x.ToString()));


        var file = File.Create("output.txt");
        var writer = new StreamWriter(file);
        foreach (var number in list)
        {
            writer.WriteLine(number);
        }

        // Sorting.MergeSort(list);
        // Logger.DisplayMessage(String.Join(", ", list));
    }

}