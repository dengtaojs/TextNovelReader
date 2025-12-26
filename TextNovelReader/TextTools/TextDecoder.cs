using UtfUnknown;

namespace TextNovelReader.TextTools;

internal class TextDecoder (string filePath)
{
    public string GetFileContent()
    {
        string result = string.Empty; 
        using var fileStream = File.Open(filePath, FileMode.Open);
        if (fileStream == null) goto END;

        var buffer = new byte[fileStream.Length];
        var readCount = fileStream.Read(buffer, 0, buffer.Length);
        if (buffer.Length != readCount) goto END;

        DetectionResult charset = CharsetDetector.DetectFromBytes(buffer);
        result = charset.Detected.Encoding.GetString(buffer); 
    END:
        return result; 
    }

    public async Task<string> GetFileContentAsync()
    {
        var result = await Task.Run(() =>
        {
            return this.GetFileContent();
        });
        return result;
    }
}
