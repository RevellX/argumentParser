﻿using argumentParser.Classes;
using argumentParser.Classes.Actions;

namespace argumentParser;

public class Program
{
    public static void Main(string[] args)
    {
        ArgumentParser.Parse(args);
        // ArgumentParser.DisplayArgs();


        if (ArgumentParser.Has("action") || ArgumentParser.Has("a"))
        {
            Argument? switchArg = ArgumentParser.Has("action") ? ArgumentParser.Get("action") : ArgumentParser.Get("a");

            switch (switchArg?.GetValue())
            {
                case "fibonacci":
                    new Fibonacci().Run();
                    break;
                case "multithreading":
                    new Multithreading().Run();
                    break;
                case "test":
                    TestMethod();
                    break;
                default:
                    Logger.DisplayMessage(Messages.ACTION_UNKNOWN + $": {switchArg?.GetValue()}", MesssageType.WARNING);
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


        // Uncomment the following lines to test the ArgumentParser functionali
        // if (ArgumentParser.TryGet("fish", out Argument arg) && arg.IsArgument())
        // {
        //     Logger.DisplayMessage($"Fish: {arg.GetValue()}");
        // }
        // else
        // {
        //     if (arg == null)
        //         Logger.DisplayError("Missing \"fish\" argument");
        //     else if (arg.IsFlag())
        //         Logger.DisplayError("Missing \"fish\" value");
        // }
    }

}