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

        var findOptions = BuildOptions(args);
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