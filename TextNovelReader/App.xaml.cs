using Microsoft.Extensions.DependencyInjection;

namespace TextNovelReader;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell())
        {
            Title = "Text Novel Reader"
        };
        return window; 
    }
}
