namespace DFind;

/// <summary>
/// Decorator design pattern
/// - Add extra layter of filter wrapping line
/// </summary>
public class FilteredLineSource : ILineSource
{
    private readonly ILineSource parent;
    private Func<Line, bool> filter;

    public FilteredLineSource(ILineSource parent, Func<Line, bool> filter = null)
    {
        this.parent = parent ?? throw new ArgumentException(nameof(parent));
        this.filter = filter;
    }

    public string Name => parent.Name;

    public Line? ReadLine()
    {
        // return readline from parent if filter equals null
        if (filter == null)
        {
            return parent.ReadLine();
        }

        var line = parent.ReadLine();

        // if don't have line, return null
        if (line == null)
        {
            return null;
        }
        else
        {
            // looping until reaching line
            while (line != null && filter(line) == false)
            {
                line = parent.ReadLine();
            }

            return line;
        }
    }

    public void Open()
    {
        parent.Open();
    }

    public void Close()
    {
        parent.Close();
    }
}