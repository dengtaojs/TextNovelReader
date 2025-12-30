using System.Collections.ObjectModel;
using TextNovelReader.Models;
using TextNovelReader.Service;
using TextNovelReader.ViewModel;

namespace TextNovelReader.Views;

public partial class BookContentsPage : ContentPage, IBackButtonHandler
{
    private readonly ReaderViewModel _viewModel;
    private readonly IEnumerable<Chapter>? _chaptersRepo;

    public BookContentsPage(ReaderViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();

        this.Title = _viewModel.CurrentBook?.Name ?? "目录";
        _chaptersRepo = _viewModel.CurrentBook?.GetChapters();

        LoadChapterAsync(); 
    }

    private async void LoadChapterAsync()
    {
        //var result = await Task.Run(() => _chaptersRepo?.ToList());
        //if (result == null)
        //    return;
        //this.ChaptersCollectionView.ItemsSource = result;

        await Task.Delay(50);
        this.ChaptersCollectionView.ItemsSource = _chaptersRepo;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Grid grid && grid.BindingContext is Chapter chapter)
        {
            _viewModel.CurrentChapter = chapter;
            await Shell.Current.GoToAsync("chapter_detail");
        }
    }

    public async void OnSystemBackButtonPressed()
    {
        await Shell.Current.GoToAsync("..");
    }
}