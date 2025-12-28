using System.Collections.ObjectModel;
using System.Text.Json;
using TextNovelReader.Models;

namespace TextNovelReader.Service;

public class ReaderService
{
    public ObservableCollection<Book> Books { get; } = [];

    public ObservableCollection<Chapter> CurrentBookChapters { get; } = []; 

    public Book? CurrentBook { get; set; }
    public Chapter? CurrentChapter { get; set; }

    public bool NeedUpdateBooks { get; set; }

    public void SaveBooks()
    {
        var savedBooks = Preferences.Default.Get("books_count", 0);

        // 先保存
        for (int i = 0; i < Books.Count; i++)
        {
            var key = $"book_{i}";
            var json = JsonSerializer.Serialize<Book>(Books[i]);
            Preferences.Default.Set(key, json); 
        }
        Preferences.Default.Set("books_count", Books.Count); 

        // 删除未使用的内容
        for(int i = Books.Count; i < savedBooks; ++i)
        {
            var key = $"book_{i}";
            Preferences.Default.Clear(key); 
        }
    }

    public void ReadBooks()
    {
        var savedBooks = Preferences.Default.Get("books_count", 0);
        this.Books.Clear(); 

        for (int i = 0; i < savedBooks; i++)
        {
            var json = Preferences.Default.Get($"book_{i}", "");
            if (string.IsNullOrEmpty(json))
                continue;
            var book = JsonSerializer.Deserialize<Book>(json);
            if (book == null) continue;
            Books.Add(book); 
        }
    }

}
