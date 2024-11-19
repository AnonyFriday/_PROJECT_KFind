using System.Text.RegularExpressions;

namespace DFind;

public class LineSourceFactory
{
    /// <summary>
    /// Handles "*.*", concrete filename, content from consoleline
    /// </summary>
    /// <param name="path"></param>
    /// <param name="skipOfflineFiles"></param>
    /// <returns></returns>
    public static ILineSource[] CreateInstance(string path, bool skipOfflineFiles)
    {
        // If path null is null
        if (string.IsNullOrEmpty(path))
        {
            return [new ConsoleLineSource()];
        }

        // Get all files from path
        else
        {
            // Handling having or without path separator
            string pattern = string.Empty;
            int idx = path.LastIndexOf(Path.PathSeparator);

            if (idx == -1)
            {
                // find English *.txt /c /v -> path = ., pattern = *.txt 
                // find English * /c /v  -> path = ., pattern = *
                pattern = path;
                path = ".";
            }
            else
            {
                // find English "C:\\Documents\\*.*"-> path = C:\\Documents\\, pattern = *.*
                // find English "C:\\Documents\\*.txt"-> path = C:\\Documents\\, pattern = *.txt

                pattern = path.Substring(idx + 1);
                path = path.Substring(0, idx);
            }

            // Get files from Data/text/{pattern}
            var dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                var files = dir.GetFiles(pattern);
                if (skipOfflineFiles)
                {
                    // Include only offline files
                    files = files
                        .Where(f => !f.Attributes.HasFlag(FileAttributes.Offline))
                        .ToArray();
                }

                // Return an array of FileLineSource instance
                return files.Select(f => new FileLineSource(f.FullName, f.Name)).ToArray();
            }
        }

        return [];
    }
}