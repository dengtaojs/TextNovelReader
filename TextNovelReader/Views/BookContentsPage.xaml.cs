using System.Collections.ObjectModel;
using TextNovelReader.Models;
using TextNovelReader.Service;
using TextNovelReader.ViewModel;

namespace TextNovelReader.Views;

public partial class BookContentsPage : ContentPage, IBackButtonHandler
{
	private readonly ReaderViewModel _viewModel;
	private readonly IEnumerable<Chapter>? _chaptersRepo = null;
	private bool _isLoading = false; 
	int _pageIndex = 0;
	const int PageSize = 50;

	public BookContentsPage(ReaderViewModel viewModel)
	{
		_viewModel = viewModel;
		InitializeComponent();

		if (_viewModel.IsContentsValid == false)
			_viewModel.Chapters.Clear(); 

		this.Title = _viewModel.CurrentBook?.Name ?? "目录";
		this.ChaptersCollectionView.ItemsSource = _viewModel.Chapters;
		_chaptersRepo = _viewModel.CurrentBook?.GetChapters();

		LoadChaptersAsync(); 
	}

	private async void LoadChaptersAsync()
	{
		await Task.Run(LoadPageChapters);
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		if (sender is Grid grid && grid.BindingContext is Chapter chapter)
		{
			_viewModel.CurrentChapter = chapter;
			await Shell.Current.GoToAsync("chapter_detail"); 
		}
    }

	private void LoadPageChapters()
	{
		if (_chaptersRepo == null)
			return;

		if (_isLoading)
			return;

		_isLoading = true;

		var start = _pageIndex * PageSize;
		var end = Math.Min(start + PageSize, _viewModel.Chapters.Count);
		foreach (var chapter in _chaptersRepo)
		{
			_viewModel.Chapters.Add(chapter);
			if (++start == end)
				break; 
		}
		_pageIndex++;
		_isLoading = false;
	}

    public async void OnSystemBackButtonPressed()
    {
		await Shell.Current.GoToAsync("..");
    }

    private void ChaptersCollectionView_RemainingItemsThresholdReached(object sender, EventArgs e)
    {
		LoadPageChapters(); 
    }
}