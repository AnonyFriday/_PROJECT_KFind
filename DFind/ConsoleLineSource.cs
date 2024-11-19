namespace DFind;

public class ConsoleLineSource : ILineSource
{
    private int lineNumber = 0;
    public string Name { get; set; } = string.Empty;

    public Line? ReadLine()
    {
        var s = Console.ReadLine();

        if (s == null)
        {
            return null;
        }
        else
        {
            return new Line() { LineNumber = ++lineNumber, Text = s };
        }
    }

    public void Open()
    {
        throw new NotImplementedException();
    }

    public void Close()
    {
        throw new NotImplementedException();
    }
}