using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TextNovelReader.Models;

namespace TextNovelReader.Service;

public class ReaderService
{
    public ObservableCollection<Book> Books { get; } = [];

    public ObservableCollection<Chapter> CurrentBookChapters { get; } = []; 

}
