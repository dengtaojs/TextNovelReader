using TextNovelReader.Service;

namespace TextNovelReader;

public partial class App : Application
{
    public ReaderService? Service { get; set; }

    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window()
        {
            Title = "Text Novel Reader",
            Page = new AppShell()
        };
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnSleep()
    {
        Service?.SaveBooks();
        base.OnSleep();
    }
}
