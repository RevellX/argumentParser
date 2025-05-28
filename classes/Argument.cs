namespace argumentParser.Classes;

public enum ArgumentType
{
    ARGUMENT,
    FLAG
}

public class Argument
{
    private ArgumentType _type;
    private string _stringValue = "";

    public Argument(ArgumentType argumentType, string value)
    {
        _type = ArgumentType.ARGUMENT;
        _stringValue = value;
    }

    public Argument(ArgumentType argumentType)
    {
        _type = ArgumentType.FLAG;
    }

    public ArgumentType GetArgumentType()
    {
        return _type;
    }

    public string GetValue()
    {
        return _stringValue;
    }
}