using System.Collections.ObjectModel;
using TextNovelReader.Models;
using TextNovelReader.Service;

namespace TextNovelReader.Pages;

[QueryProperty("CollectionIndex", "index")]
public partial class BookContentsPage : ContentPage
{
    private readonly ReaderService _service;
	public Book? _currentBook = null;

	private int _collectionIndex = -1;
	public int CollectionIndex
	{
		get => _collectionIndex;
		set => OnCollectionIndexChanged(value); 
	}

	public BookContentsPage(ReaderService service)
	{
		InitializeComponent();
        _service = service;
        this.ChaptersCollectionView.ItemsSource = _service.CurrentBookChapters;
    }

    // TODO 
    // 避免多次调用这个函数
	public async void OnCollectionIndexChanged(int value)
	{
        if (_collectionIndex == value)
            return; 

        _collectionIndex = value;
        _currentBook = _service.Books[_collectionIndex]; 

        if (_currentBook != null)
        {
            this.Title = _currentBook.Name;
            var chapters = await _currentBook.GetChaptersAsync();
            _service.CurrentBookChapters.Clear(); 

            foreach (var chapter in chapters)
            {
                _service.CurrentBookChapters.Add(chapter);
            }
          
        }
    }

    private async void ChaptersCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return; 

        if(e.CurrentSelection[0] is Chapter chapter)
        {
            await Shell.Current.GoToAsync($"chapter_detail?index={chapter.Index}");
        }
    }
}