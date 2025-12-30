using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using TextNovelReader.TextTools;

namespace TextNovelReader.Models;

public partial class Book : BindableBase
{
    private string? _name;

    [JsonIgnore]
    public string Name
        => _name ??= GetName();

    [JsonInclude]
    public string FilePath { get; set; } = string.Empty;

    [JsonInclude]
    public int ChapterIndex { get; set; }

    [JsonInclude]
    public int ReadingPosition { get; set; }

    private string GetName()
    {
        if (string.IsNullOrEmpty(FilePath)) return string.Empty;

        var fileInfo = new FileInfo(FilePath);
        return fileInfo.Name;
    }

    public List<Chapter> GetChapters()
    {
        List<Chapter> result = [];

        if (File.Exists(FilePath) != true)
            return result;

        var textFileReader = new TextFileReader(FilePath); 

        Chapter currentChapter = new("前言", string.Empty);
        result.Add(currentChapter); 

        StringBuilder textBuilder = new();

        foreach(var line in textFileReader.GetLines())
        {
            int upper = Math.Min(12, line.Length);
            if (IsTitle(line.AsSpan()[..upper]))
            {
                currentChapter.Text = textBuilder.ToString();

                textBuilder = textBuilder.Clear();
                currentChapter = new(line.Trim(), string.Empty);
                result.Add(currentChapter); 
            }
            else
            {
                textBuilder.AppendLine(line);
            }
        }

        currentChapter.Text = textBuilder.ToString();
        return result;
    }

    public async Task<List<Chapter>> GetChaptersAsync()
    {
        return await Task.Run(() => GetChapters());
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
