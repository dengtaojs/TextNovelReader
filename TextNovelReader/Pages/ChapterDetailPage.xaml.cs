using TextNovelReader.Service;

namespace TextNovelReader.Pages;

[QueryProperty("ChapterIndex", "index")]
public partial class ChapterDetailPage : ContentPage
{
	private readonly ReaderService _service; 
	public ChapterDetailPage(ReaderService service)
	{
		_service = service; 
		InitializeComponent();
	}

	private int _chapterIndex = -1;
	public int ChapterIndex
	{
		get => _chapterIndex;
		set => SetChapterIndex(value); 
	}

	private void SetChapterIndex(int value)
	{
		if (_chapterIndex == value)
			return; 

		_chapterIndex = value;
		var chapter = _service.CurrentBookChapters[value];
		this.Title = chapter.Title;
		this.ChapterTextLabel.Text = chapter.Text; 
	}
}