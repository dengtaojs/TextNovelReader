using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using TextNovelReader.Models;
using TextNovelReader.Pages;

namespace TextNovelReader;

public partial class App : Application
{
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
}
