
using TextNovelReader.Views;

namespace TextNovelReader;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("settings", typeof(SettingsPage));
        Routing.RegisterRoute("home", typeof(HomePage));
        Routing.RegisterRoute("book_contents", typeof(BookContentsPage));
        Routing.RegisterRoute("chapter_detail", typeof(ChapterDetailPage));

        if (Platform.CurrentActivity is MainActivity mainActivity)
        {
            MainActivity.Current = mainActivity; 
            mainActivity.RegisterEvents(); 
        }
    }
}
