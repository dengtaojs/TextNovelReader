namespace TextNovelReader.Models;

internal class Chapter
{
    public string Title { get; set; }
    public string Text { get; set; }
    public int Index { get; set; }

    public Chapter(string title, string text, int index)
    {
        Title = title;
        Text = text;
        Index = index;
    }

    public Chapter()
    {
        Title = string.Empty;
        Text = string.Empty;
        Index = 0; 
    }
}
