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
        return new Window(new AppShell());
    }

    protected override void OnSleep()
    {
        Service?.SaveBooks();
        base.OnSleep();
    }
}
