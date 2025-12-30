using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TextNovelReader.Service;
using TextNovelReader.TextTools;
using TextNovelReader.ViewModel;

namespace TextNovelReader.Views;

public partial class ChapterDetailPage : ContentPage, IBackButtonHandler
{
    private readonly ReaderViewModel _viewModel;
    private readonly ObservableCollection<string> _pages = [];

    public ChapterDetailPage(ReaderViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();

        if (_viewModel.CurrentChapter != null)
        {
            this.ChapterTitleLabel.Text = _viewModel.CurrentChapter.Title;
            this.ReaderTextLabel.Text = _viewModel.CurrentChapter.Text; 
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
        Shell.SetNavBarIsVisible(this, false);
        var activity = Platform.CurrentActivity;
        if (activity != null)
        {
            Platforms.Android.FullScreenHelper.EnterFullScreen(activity);
        }
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Shell.SetTabBarIsVisible(this, true);
        Shell.SetNavBarIsVisible(this, true); 

        var activity = Platform.CurrentActivity;
        if (activity != null)
        {
            Platforms.Android.FullScreenHelper.ExitFullScreen(activity);
        }
    }
}