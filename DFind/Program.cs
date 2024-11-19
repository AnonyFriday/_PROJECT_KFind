using Program;

namespace DFind;

/// <summary>
/// Syntax: find [/v] [/c] [/n] [/i] [/off[line]] <"string"> [[<drive>:][<path>]<filename>[...]]
///
/// Examples:
/// find "you" text/a-midsummer-nights-dream_TXT_FolgerShakespeare.txt
/// - find a word "you" in the file at relative path "text/a-midsummer-nights-dream_TXT_FolgerShakespeare.txt"
///
/// find "you" text/a-midsummer-nights-dream_TXT_FolgerShakespeare.txt /v /c /n /i
/// - /v: Display lines that don't contain "you"
/// - /c: Counts the lines
/// - /n: Line number that appears the words
/// - /i: not case-sensitive
///
/// find "English" *.txt /v /i /c
/// - counts how many lines of each file that doesn't contain English word, case insensitive
///
/// Decorator Design Pattern
/// Abstract Factory
///
/// Logics
/// 1. Program can find string on Console or File
/// 
///
/// </summary>
public class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("FIND: Parameter format not correct");
            return;
        }

        // Get multiple Options from the console
        var findOptions = BuildOptions(args);

        // If in help mode
        if (findOptions.IsHelpMode)
        {
            Console.WriteLine(@"
Searches for a text string in a file or files.

FIND [/V] [/C] [/N] [/I] [/OFF[LINE]] ""string"" [[drive:][path]filename[ ...]]

  /V         Displays all lines NOT containing the specified string.
  /C         Displays only the count of lines containing the string.
  /N         Displays line numbers with the displayed lines.
  /I         Ignores the case of characters when searching for the string.
  /OFF[LINE] Do not skip files with offline attribute set.
  ""string""   Specifies the text string to find.
  [drive:][path]filename
             Specifies a file or files to search.

If a path is not specified, FIND searches the text typed at the prompt
or piped from another command.
");
            return;
        }

        var sources = LineSourceFactory.CreateInstance(findOptions.Path, findOptions.IsSkipOfflineFiles);

        foreach (var source in sources)
        {
            ProcessSource(source, findOptions);
        }
    }

    /// <summary>
    /// For each line, print out and advance on next line
    /// </summary>
    /// <param name="source"></param>
    /// <param name="findOptions"></param>
    private static void ProcessSource(ILineSource source, FindOptions findOptions)
    {
        // Appply Decorator Design Pattern for source filter
        var stringCaseComparision = findOptions.IsCaseSensitive
            ? StringComparison.CurrentCulture
            : StringComparison.CurrentCultureIgnoreCase;

        source = new FilteredLineSource(source, line => !findOptions.IsFindDontContain
            ? line.Text.Contains(findOptions.StringToFind, stringCaseComparision)
            : !line.Text.Contains(findOptions.StringToFind));

        Console.WriteLine($"\n----------- {source.Name.ToUpper()}\n");

        try
        {
            // Open Source
            source.Open();

            // Read each line
            var line = source.ReadLine();

            while (line != null)
            {
                Print(line, findOptions.IsShowLineNumber);
                line = source.ReadLine();
            }
        }
        finally
        {
            // Close Source
            source.Close();
        }
    }

    private static void Print(Line line, bool isPrintLineNumber)
    {
        if (isPrintLineNumber)
        {
            Console.WriteLine($"{line.LineNumber}: {line.Text}");
        }
        else
        {
            Console.WriteLine($"{line.Text}");
        }
    }

    public static FindOptions BuildOptions(string[] args)
    {
        var options = new FindOptions();
        foreach (var arg in args)
        {
            if (arg.StartsWith("/"))
            {
                switch (arg)
                {
                    case "/v":
                        options.IsFindDontContain = true;
                        break;
                    case "/c":
                        options.IsCountMode = true;
                        break;
                    case "/n":
                        options.IsShowLineNumber = true;
                        break;
                    case "/i":
                        options.IsCaseSensitive = true;
                        break;
                    case "/offline":
                    case "/off":
                        options.IsSkipOfflineFiles = true;
                        break;
                    case "/?":
                        options.IsHelpMode = true;
                        break;
                }
            }
            else if (string.IsNullOrEmpty(options.StringToFind))
            {
                options.StringToFind = arg;
            }
            else if (string.IsNullOrEmpty(options.Path))
            {
                options.Path = arg;
            }
            else
            {
                throw new ArgumentException(arg);
            }
        }

        return options;
    }
}