using TextNovelReader.ViewModels;

namespace TextNovelReader.Views;

[QueryProperty("FilePathHash", "hash")]
public partial class BookContentsPage : ContentPage
{
	private readonly BookContentsPageViewModel _viewModel;
	private int _filePathHash;
	public BookContentsPage(BookContentsPageViewModel viewModel)
	{
		_viewModel = viewModel;
		InitializeComponent();
	}

	public BookContentsPageViewModel ViewModel => _viewModel; 

	public int FilePathHash
	{
		get => _filePathHash;
		set => _filePathHash = value; 
	}

	public void SetCurrentBook()
	{
		if (FilePathHash == 0) return; 
		
	}
}