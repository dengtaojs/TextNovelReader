using TextNovelReader.ViewModel;

namespace TextNovelReader.Views;

public partial class BookContentsPage : ContentPage
{
	private readonly ReaderViewModel _viewModel; 
	public BookContentsPage(ReaderViewModel viewModel)
	{
		_viewModel = viewModel;
		InitializeComponent();

		this.ChaptersCollectionView.ItemsSource = _viewModel.Chapters;
		this.Title = _viewModel.CurrentBook?.Name ?? "目录";
		this.LoadChaptersAsync(); 
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }

	private async void LoadChaptersAsync()
	{
		if (_viewModel.IsContentsValid) return;
		_viewModel.Chapters.Clear();

		if (_viewModel.CurrentBook == null) return;
		var chapters = await _viewModel.CurrentBook.GetChaptersAsync(); 
		foreach (var chapter in chapters)
		{
			_viewModel.Chapters.Add(chapter); 
		}
	}
}