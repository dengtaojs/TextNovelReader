using System.Collections.ObjectModel;
using System.Windows.Input;
using TextNovelReader.Models;

namespace TextNovelReader.ViewModel;

public class ReaderViewModel : BindableBase
{

    public ObservableCollection<Book> Books { get;} = [];
    public List<Chapter> Chapters { get; set; } = []; 
    public Book? CurrentBook { get; set; }
    public Chapter? CurrentChapter { get; set; }

}
