using System.Text;
using UtfUnknown;

namespace TextNovelReader.TextTools;

public static class TextDecoder
{
    public static Encoding GetEncoding(string filePath, int maxBytesToRead)
    {
        using var stream = new FileStream(filePath, FileMode.Open); 
        DetectionResult result = CharsetDetector.DetectFromStream(stream, maxBytesToRead);
        return result.Detected.Encoding;
    }

}
