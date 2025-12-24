using System.Windows.Input;
using TextNovelReader.Pages;

namespace TextNovelReader;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        BackCommand = new Command(OnBackCommandInvoked); 
       
    }

    public ICommand BackCommand { get; }

    private async void OnBackCommandInvoked()
    {
        if(Shell.Current.Navigation.NavigationStack.Count > 1)
        {
            await Current.Navigation.PopAsync(true);
        }
    }

}
