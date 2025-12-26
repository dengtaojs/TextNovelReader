using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using TextNovelReader.Models;
using TextNovelReader.Service;

namespace TextNovelReader.Pages;

public partial class HomePage : ContentPage
{
	private readonly ReaderService _service;
	private bool _isSwipping = false; 
	public ObservableCollection<Book> Books
		=> _service.Books;

	public HomePage(ReaderService service)
	{
		InitializeComponent();

		_service = service;
		(Application.Current as App)?.Service = _service;
		_service.ReadBooks();

		HistoryCollection.ItemsSource = Books;
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
			var book = new Book() { FilePath = result.FullPath };
			this.AddBookToServiceCollection(book); 

			await Shell.Current.GoToAsync($"book_contents?index={book.CollectionIndex}", false);
		}
    }

	private void AddBookToServiceCollection(Book book)
	{
		var list = _service.Books.ToList();
		list.Reverse();

		if (list.Find((x => x.FilePath == book.FilePath)) != null)
			return;
		list.Add(book); 

		int i = -1; 
		_service.Books.Clear();
		foreach(var b in list)
		{
			b.CollectionIndex = ++i;
			_service.Books.Add(b);
		}
	}

    private async void HistoryCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		if (_isSwipping)
		{
			HistoryCollection.SelectedItem = null;
			return; 
		}

		if (e.CurrentSelection.Count != 1)
			return;
		if (e.CurrentSelection[0] is Book book)
		{
			await Shell.Current.GoToAsync($"book_contents?index={book.CollectionIndex}", false);
			HistoryCollection.SelectedItem = null;
		}
    }

    private void DeleteBook(object sender, EventArgs e)
    {
		if(sender is Button button)
		{
			if (button.BindingContext is Book book)
			{
				_service.Books.Remove(book);
				for (int i = 0; i < _service.Books.Count; i++)
				{
					_service.Books[i].CollectionIndex = i;
				}
			}
		}
    }

    private void SwipeView_SwipeStarted(object sender, SwipeStartedEventArgs e)
    {
		_isSwipping = true;
    }

    private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
    {
		_isSwipping = false;
    }
}