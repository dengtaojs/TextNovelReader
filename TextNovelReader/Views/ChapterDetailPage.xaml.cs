using TextNovelReader.Service;
using TextNovelReader.ViewModel;

namespace TextNovelReader.Views;

public partial class ChapterDetailPage : ContentPage, IBackButtonHandler
{
    private readonly ReaderViewModel _viewModel;
    public ChapterDetailPage(ReaderViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();

        if (_viewModel.CurrentChapter != null)
        {
            this.Title = _viewModel.CurrentChapter.Title;
            this.ChapterTextLabel.Text = _viewModel.CurrentChapter.Text; 
        }
    }

    public async void OnSystemBackButtonPressed()
    {
        await Shell.Current.GoToAsync("..");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetTabBarIsVisible(this, false);
        var activity = Platform.CurrentActivity; 
        if(activity != null)
        {
            Platforms.Android.FullScreenHelper.EnterFullScreen(activity); 
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Shell.SetTabBarIsVisible(this, true);

        var activity = Platform.CurrentActivity; 
        if(activity != null)
        {
            Platforms.Android.FullScreenHelper.ExitFullScreen(activity); 
        }
    }


}