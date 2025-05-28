using System.Reflection.Metadata;
using System.Text;

namespace argumentParser.Classes;

public sealed class ArgumentParser
{
    //  Items are being stored as KeyValuePair<TKey, TValue>
    private static readonly Dictionary<string, Argument> _args = new();
    private static bool _lock = false;
    private const char ARGUMENT_PREFIX = '-';

    private ArgumentParser() { }

    private static string getValueFromArgs(string[] args, int startingIndex)
    {
        var sb = new StringBuilder();
        for (int i = startingIndex; i < args.Length && !args[i].StartsWith(ARGUMENT_PREFIX); i++)
        {
            sb.Append(args[i]);
            if (args[i].StartsWith(ARGUMENT_PREFIX))
                sb.Append(' ');
        }
        return sb.ToString().Trim('\"');
    }

    public static void Parse(string[] args)
    {  /*
        * From -input file.exe -output test file.exe -mode "strict mode"
        * You get this:
        * {
        *   input: "file.exe"
        *   output: "test"
        *   mode: "strict
        * }
        */
        if (!_lock)
        {
            _lock = true;
            for (int i = 0; i < args.Length; i++)
            {
                // Argument of Flag
                if (args[i].StartsWith("-"))
                {
                    // Argument with value
                    if (i < args.Length - 1 && !args[i + 1].StartsWith(ARGUMENT_PREFIX))
                    {
                        _args[args[i].TrimStart(ARGUMENT_PREFIX)] = new Argument(ArgumentType.ARGUMENT, getValueFromArgs(args, i + 1));
                    }
                    // Flag
                    else
                    {
                        _args[args[i].TrimStart(ARGUMENT_PREFIX)] = new Argument(ArgumentType.FLAG);
                    }
                }
            }
        }
    }

    public static Argument Get(string key)
    {
        return _args.TryGetValue(key, out var value) ? value : null;
    }

    public static bool Has(string key)
    {
        return _args.ContainsKey(key);
    }

    public static void DisplayArgs()
    {
        foreach (KeyValuePair<string, Argument> pair in _args)
        {

            if (pair.Value.GetArgumentType() == ArgumentType.ARGUMENT)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value.GetValue()}");
            }
            else
            {
                Console.WriteLine($"{pair.Key}");
            }
        }
    }
}
