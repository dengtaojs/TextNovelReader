using System.Text;
using System.Text.RegularExpressions;
using TextNovelReader.TextTools;

namespace TextNovelReader.Models;

public partial class Book(string filePath)
{
    private readonly TextDecoder _decoder = new(filePath);
    private string? _bookName;

    public string Name
        => _bookName ??= GetBookName();

    public string FilePath
        => filePath;

    public int ReadingChapterIndex { get; set; }
    public int ReadingPosition { get; set; }
    public int CollectionIndex { get; set; }


    private string GetBookName()
    {
        var fileInfo = new FileInfo(filePath);
        var fileName = fileInfo.Name;

        if (fileName.EndsWith(".txt")) return fileName[..^4];
        return fileName;
    }

    public List<Chapter> GetChapters()
    {
        List<Chapter> result = [];

        var fileContent = _decoder.GetFileContent();
        using var reader = new StringReader(fileContent);

        if (string.IsNullOrEmpty(fileContent)) goto END;

        Chapter currentChapter = new(GetBookName(), string.Empty, 0);
        result.Add(currentChapter);

        string? line = null;
        StringBuilder textBuilder = new();
        while (true)
        {
            line = reader.ReadLine();
            if (line == null) break;
            if (string.IsNullOrEmpty(line)) continue;

            int upper = Math.Min(12, line.Length);
            if (IsTitle(line.AsSpan()[..upper]))
            {
                currentChapter.Text = textBuilder.ToString();
                textBuilder = textBuilder.Clear();
                currentChapter = new(line, string.Empty, result.Count);
                result.Add(currentChapter);
            }
            else
            {
                textBuilder.AppendLine(line);
            }
        }
        currentChapter.Text = textBuilder.ToString();
    END:
        return result;
    }

    public async Task<List<Chapter>> GetChaptersAsync()
    {
        var result = await Task.Run(() =>
        {
            return this.GetChapters();
        });
        return result; 
    }

    private static bool IsTitle(ReadOnlySpan<char> input)
    {
        var regex = TitleRegex();
        var result = regex.Count(input);
        return result != 0;
    }

    [GeneratedRegex(@"第\s?[\d一二三四五六七八九十百千万零]+\s?章")]
    private static partial Regex TitleRegex();
}
