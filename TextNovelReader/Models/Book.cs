using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using TextNovelReader.TextTools;

namespace TextNovelReader.Models;

public partial class Book : BindableBase
{
    private string? _name;
    private int _collectionIndex = 0; 

    [JsonIgnore]
    public string Name
        => _name ??= GetName();

    [JsonInclude]
    public string FilePath { get; set; } = string.Empty;

    [JsonInclude]
    public int ReadingChapterIndex { get; set; }

    [JsonInclude]
    public int ReadingPosition { get; set; }

    [JsonIgnore]
    public string CollectionTitle
        => $"{CollectionIndex}. {Name}"; 

    [JsonInclude]
    public int CollectionIndex
    {
        get => _collectionIndex;
        set
        {
            _collectionIndex = value;
            RaisePropertyChanged(nameof(CollectionTitle)); 
        }
    }

    private string GetName()
    {
        if (string.IsNullOrEmpty(FilePath)) return string.Empty; 

        var fileInfo = new FileInfo(FilePath);
        var fileName = fileInfo.Name;

        if (fileName.EndsWith(".txt")) return fileName[..^4];
        return fileName;
    }

    public List<Chapter> GetChapters()
    {
        List<Chapter> result = [];
        if (File.Exists(FilePath) != true)
            return result; 

        var decoder = new TextDecoder(FilePath); 
        var fileContent = decoder.GetFileContent();
        using var reader = new StringReader(fileContent);

        if (string.IsNullOrEmpty(fileContent))
            return result; 

        Chapter currentChapter = new(Name, string.Empty, 0);
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
