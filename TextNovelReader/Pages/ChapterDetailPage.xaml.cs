using TextNovelReader.Service;

namespace TextNovelReader.Pages;

public partial class ChapterDetailPage : ContentPage, IBackButtonHandler
{
    private readonly ReaderService _service;
    public ChapterDetailPage(ReaderService service)
    {
        _service = service;
        InitializeComponent();
        if (_service.CurrentChapter != null)
        {
            this.Title = _service.CurrentChapter.Title;
            this.ChapterTextLabel.Text = _service.CurrentChapter.Text; 
        }
    }

    async void IBackButtonHandler.OnBackButtonPressed()
    {
        await Shell.Current.GoToAsync("..", false); 
    }
}