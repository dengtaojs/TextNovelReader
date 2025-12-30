using System.Collections.ObjectModel;
using TextNovelReader.Models;
using TextNovelReader.Service;
using TextNovelReader.ViewModel;

namespace TextNovelReader.Views;

public partial class BookContentsPage : ContentPage, IBackButtonHandler
{
    private readonly ReaderViewModel _viewModel;
    private readonly IEnumerable<Chapter>? _chaptersRepo;
    private readonly ObservableCollection<Chapter> _chapters = []; 


    public BookContentsPage(ReaderViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();

        this.Title = _viewModel.CurrentBook?.Name ?? "目录";
        this.ChaptersCollectionView.ItemsSource = _chapters;
        _chaptersRepo = _viewModel.CurrentBook?.GetChapters();

        LoadChapterAsync(); 
    }

    private async void LoadChapterAsync()
    {
        var result = await Task.Run(() => _chaptersRepo?.ToList());
        if (result == null)
            return;
        _chapters.Clear(); 
        foreach (var chapter in result)
        {
            _chapters.Add(chapter); 
        }
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