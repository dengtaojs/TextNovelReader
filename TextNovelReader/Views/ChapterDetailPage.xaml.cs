using TextNovelReader.ViewModel;

namespace TextNovelReader.Views;

public partial class ChapterDetailPage : ContentPage
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


}