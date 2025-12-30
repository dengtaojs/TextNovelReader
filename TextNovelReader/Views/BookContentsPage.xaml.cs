using TextNovelReader.Models;
using TextNovelReader.Service;
using TextNovelReader.ViewModel;

namespace TextNovelReader.Views;

public partial class BookContentsPage : ContentPage, IBackButtonHandler
{
    private readonly ReaderViewModel _viewModel;

    public BookContentsPage(ReaderViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();

        this.Title = _viewModel.CurrentBook?.Name ?? "目录";
        LoadChapterAsync(); 
    }

    private async void LoadChapterAsync()
    {
        if (_viewModel.CurrentBook == null) return; 

        _viewModel.Chapters = await _viewModel.CurrentBook.GetChaptersAsync();
        this.ChaptersCollectionView.ItemsSource = _viewModel.Chapters;
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