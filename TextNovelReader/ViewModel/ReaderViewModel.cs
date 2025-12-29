using System.Collections.ObjectModel;
using System.Windows.Input;
using TextNovelReader.Models;

namespace TextNovelReader.ViewModel;

public class ReaderViewModel : BindableBase
{

    public ObservableCollection<Book> Books { get; } = [];
    public ObservableCollection<Chapter> Chapters { get; } = []; 
    public Book? CurrentBook { get; set; }
    public Chapter? CurrentChapter { get; set; }
    public bool IsContentsValid { get; set; } = false;

}
