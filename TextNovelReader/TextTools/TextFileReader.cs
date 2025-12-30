using System;
using System.Collections.Generic;
using System.Text;

namespace TextNovelReader.TextTools;

public class TextFileReader(string filePath)
{
    public IEnumerable<string> GetLines()
    {
        var encoding = TextDecoder.GetEncoding(filePath, 100);

        using var stream = new FileStream(filePath, FileMode.Open);
        var byteBuff = new byte[stream.Length];
        var bytesRead = stream.Read(byteBuff, 0, byteBuff.Length);

        if (bytesRead != byteBuff.Length)
            yield break;

        var fileContent = encoding.GetString(byteBuff);
        using var strReader = new StringReader(fileContent);

        string? line = null;
        while (true)
        {
            line = strReader.ReadLine();
            if (line == null) break;
            yield return line; 
        }
    }
}
