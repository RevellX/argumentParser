using System.Text;

namespace argumentParser.Classes;

public static class Logger
{
    public static string DisplayMessage(string messageContent, MesssageType messageType = MesssageType.INFO, bool dontDisplay = false)
    {
        var sb = new StringBuilder();
        sb.Append("[");
        switch (messageType)
        {
            case MesssageType.INFO:
                sb.Append("LOG");
                break;
            case MesssageType.WARNING:
                sb.Append("WAR");
                break;
            case MesssageType.ERROR:
                sb.Append("ERR");
                break;
        }
        sb.Append("]: ");
        sb.Append(messageContent);
        var str = sb.ToString();
        if (!dontDisplay)
            Console.WriteLine(str);
        return sb.ToString();
    }

    public static string DisplayWarning(string messageContent)
    {
        return DisplayMessage(messageContent, MesssageType.WARNING);
    }

    public static string DisplayError(string messageContent)
    {
        return DisplayMessage(messageContent, MesssageType.ERROR);
    }
}