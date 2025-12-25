using TextNovelReader.Pages;

namespace TextNovelReader;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("book_contents", typeof(BookContentsPage));
        Routing.RegisterRoute("chapter_detail", typeof(ChapterDetailPage)); 
    }
}
