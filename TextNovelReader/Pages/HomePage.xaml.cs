using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using TextNovelReader.Models;
using TextNovelReader.Service;

namespace TextNovelReader.Pages;

public partial class HomePage : ContentPage
{
	private readonly ReaderService _service;
	public ObservableCollection<Book> Books
		=> _service.Books;

	public HomePage(ReaderService service)
	{
		_service = service; 
		InitializeComponent();
		this.HistoryCollection.ItemsSource = Books;
	}
    private async void OpenFileButton_Clicked(object sender, EventArgs e)
    {
		var fileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>()
		{
			[DevicePlatform.Android] = ["text/plain"]
		};
        var option = new PickOptions
        {
            FileTypes = new FilePickerFileType(fileTypes)
        };

        var result = await FilePicker.Default.PickAsync(option);
		if(result != null)
		{
			var book = new Book(result.FullPath);
			this.AddBookToServiceCollection(book); 

			await Shell.Current.GoToAsync($"book_contents?index={book.CollectionIndex}");
		}
    }

	private void AddBookToServiceCollection(Book book)
	{
		var list = _service.Books.ToList();
		list.Reverse();

		int i = 0; 
		_service.Books.Clear();
		book.CollectionIndex = i;
		_service.Books.Add(book);

		foreach(var b in list)
		{
			b.CollectionIndex = ++i;
			_service.Books.Add(b);
		}
	}
}