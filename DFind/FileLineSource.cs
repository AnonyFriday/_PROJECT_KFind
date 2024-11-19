namespace DFind;

public class FileLineSource : ILineSource
{
    private readonly string path;
    private readonly string fileName;
    private readonly string pattern;
    private StreamReader reader;
    private int lineNumber = 0;

    public string Name => fileName;

    public FileLineSource(string path, string fileName)
    {
        this.path = path;
        this.fileName = fileName;
    }


    public Line? ReadLine()
    {
        if (reader == null)
        {
            throw new InvalidOperationException();
        }

        string s = reader.ReadLine();
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
        if (reader != null)
        {
            throw new InvalidOperationException("FileLineSource is already open");
        }

        lineNumber = 0;
        reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
    }

    public void Close()
    {
        if (reader != null)
        {
            reader.Close();
            reader.Dispose();
            reader = null;
        }
    }
}