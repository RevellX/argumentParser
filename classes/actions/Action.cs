namespace argumentParser.Classes.Actions;

interface IActionable
{
    public void Run();
    public string GetName();
    public string GetDescription();
}