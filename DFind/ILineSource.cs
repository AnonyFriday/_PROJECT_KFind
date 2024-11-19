using Microsoft.VisualBasic;

namespace DFind;

public interface ILineSource
{
    string Name { get; }

    Line? ReadLine();

    void Open();

    void Close();
}