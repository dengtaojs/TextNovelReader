using System.Collections.ObjectModel;
using TextNovelReader.Models;
using TextNovelReader.Service;

namespace TextNovelReader.Pages;

public partial class BookContentsPage : ContentPage, IBackButtonHandler
{
    private readonly ReaderService _service;

    public BookContentsPage(ReaderService service)
    {
        InitializeComponent();
        _service = service;

        this.ChaptersCollectionView.ItemsSource = _service.CurrentBookChapters;
        this.Title = _service.CurrentBook?.Name ?? "目录";
        LoadChaptersAsync(); 
    }

    private async void LoadChaptersAsync()
    {
        if (_service.CurrentBook == null)
            return;

        if (_service.NeedUpdateBooks)
        {
            _service.CurrentBookChapters.Clear();
            var chapters = await _service.CurrentBook.GetChaptersAsync();
            foreach (var chapter in chapters)
            {
                _service.CurrentBookChapters.Add(chapter);
            }
        }

        _service.CurrentChapter = _service.CurrentBookChapters[_service.CurrentBook.ReadingChapterIndex];
        this.ChaptersCollectionView.SelectedItem = _service.CurrentChapter;

        await Task.Delay(100);
        this.ChaptersCollectionView.ScrollTo(_service.CurrentBook.ReadingChapterIndex, position: ScrollToPosition.Center);
    }

    async void IBackButtonHandler.OnBackButtonPressed()
    {
        await Shell.Current.GoToAsync("..", false);
    }

    private async void CotentsItem_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Label button)
        {
            if (button.BindingContext is Chapter chapter)
            {
                _service.CurrentChapter = chapter;
                this.ChaptersCollectionView.SelectedItem = chapter;

                if (_service.CurrentBook != null)
                {
                    var index = _service.CurrentBookChapters.IndexOf(chapter);
                    _service.CurrentBook.ReadingChapterIndex = index;
                }
                await Shell.Current.GoToAsync($"chapter_detail", true);
            }
        }
    }
}