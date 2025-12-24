namespace TextNovelReader.Pages;

public partial class MainPage : ContentPage
{
    private bool _flashLightSwitch = false; 
	public MainPage()
	{
		InitializeComponent();
	}

    private async void GotoReaderPage(object sender, EventArgs e)
    {
		var readerPage = new ReaderPage();
		await Shell.Current.Navigation.PushAsync(readerPage, true); 
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (_flashLightSwitch)
                await Flashlight.Default.TurnOffAsync(); 
            else
                await Flashlight.Default.TurnOnAsync();
            _flashLightSwitch = !_flashLightSwitch;
        }
        catch (Exception)
        {
            return;
        }
    }
}