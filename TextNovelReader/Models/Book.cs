using System;
using System.Collections.Generic;
using System.Text;

namespace TextNovelReader.Models;

internal class Book
{
    public string Name { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty; 
    public int ReadingChapterIndex { get; set; }
    public int ReadingPosition { get; set; }
}
