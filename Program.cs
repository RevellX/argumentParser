using argumentParser.Classes;
using argumentParser.Classes.Actions;

namespace argumentParser;

public class Program
{
    public static void Main(string[] args)
    {
        ArgumentParser.Parse(args);
        ArgumentParser.DisplayArgs();


        if (ArgumentParser.Has("action") || ArgumentParser.Has("a"))
        {
            var switchArg = ArgumentParser.Has("action") ? ArgumentParser.Get("action") : ArgumentParser.Get("a");

            switch (switchArg.GetValue())
            {
                case "fibonacci":
                    Fibonacci.Run();
                    break;

                default:
                    Logger.DisplayMessage(Messages.ACTION_UNKNOWN + $": {switchArg.GetValue()}", MesssageType.WARNING);
                    break;
            }
        }
        else
        {
            Logger.DisplayMessage(Messages.ACTION_ARGUMENT_MISSING, MesssageType.ERROR);
        }
    }

}