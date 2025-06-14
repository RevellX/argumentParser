namespace argumentParser.Classes.Utils;

class FileHandler
{
    public static void WriteListToFile<T>(Stream fileStream, List<T> lines)
    {
        try
        {
            using (var writer = new StreamWriter(fileStream))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.DisplayError($"Error writing to file: {ex.Message}");
        }
    }
}