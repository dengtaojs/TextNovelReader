using TextNovelReader.Models;
using TextNovelReader.ViewModel;

namespace TextNovelReader.Views;

public partial class HomePage : ContentPage
{
	private readonly ReaderViewModel _viewModel; 
	public HomePage(ReaderViewModel viewModel)
	{
		_viewModel = viewModel; 
		InitializeComponent();
        BooksCollectionView.ItemsSource = _viewModel.Books;
	}

	public ReaderViewModel ViewModel => _viewModel;

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Grid grid && grid.BindingContext is Book book)
        {
            _viewModel.CurrentBook = book;
            await Shell.Current.GoToAsync("book_contents", false); 
        }
    }

    private async void OpenFileButton_Clicked(object sender, EventArgs e)
    {
        var fileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>()
        {
            [DevicePlatform.Android] = ["text/plain"]
        });
        var option = new PickOptions() { FileTypes = fileTypes };
        var result = await FilePicker.Default.PickAsync(option);

        if (result == null) return;
        var book = _viewModel.Books.FirstOrDefault((x) => x.FilePath == result.FullPath);
        if (book == null)
        {
            book = new() { FilePath = result.FullPath };
            _viewModel.Books.Add(book);
        }

        _viewModel.CurrentBook = book;
        await Shell.Current.GoToAsync("book_contents", false);
    }
}