namespace TextNovelReader.Models;

public class Chapter
{
    public string Title { get; set; }
    public string Text { get; set; }

    public Chapter(string title, string text)
    {
        Title = title;
        Text = text;
    }

    public Chapter()
    {
        Title = string.Empty;
        Text = string.Empty;
    }
}
