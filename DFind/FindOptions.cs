namespace Program;

public class FindOptions
{
    public string StringToFind { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public bool IsCaseSensitive { get; set; } = false;
    public bool IsFindDontContain { get; set; } = false;
    public bool IsCountMode { get; set; } = false;
    public bool IsShowLineNumber { get; set; } = false;
    public bool IsSkipOfflineFiles { get; set; } = true;
    public bool IsHelpMode { get; set; } = false;
}