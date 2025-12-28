using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using TextNovelReader.Models;
using TextNovelReader.Service;

namespace TextNovelReader.Pages;

public partial class HomePage : ContentPage
{
    private readonly ReaderService _service;
    public ObservableCollection<Book> Books => _service.Books;

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
        if (result != null)
        {
            var book = UpdateBooksAndRetriveCurrentBook(result.FullPath);
            _service.NeedUpdateBooks = book != _service.CurrentBook; 
            await Shell.Current.GoToAsync($"book_contents", false);
        }
    }

    private Book? UpdateBooksAndRetriveCurrentBook(string filePath)
    {

        var list = _service.Books.ToList();
        var result = list.Find((x) => x.FilePath == filePath);
        if (result == null)
        {
            result = new Book() { FilePath = filePath };
            list.Add(result); 
        }

        list.Reverse();
        _service.Books.Clear();
        int i = 0;
        foreach (var b in list)
        {
            b.CollectionIndex = ++i;
            _service.Books.Add(b);
        }

        return result; 
    }


    private async void BokkItem_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Label label && label.BindingContext is Book book)
        {
            _service.NeedUpdateBooks = _service.CurrentBook != book;
            _service.CurrentBook = book;
            HistoryCollection.SelectedItem = book;
            await Shell.Current.GoToAsync($"book_contents", false);
        }
    }

    private void DeleteBookItem_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Label label && label.BindingContext is Book book)
        {
            _service.Books.Remove(book);
            ResetBooksIndex(); 
        }
    }

    private void ResetBooksIndex()
    {
        int i = 1;
        foreach (var book in _service.Books)
        {
            book.CollectionIndex = i;
            ++i; 
        }
    }
}