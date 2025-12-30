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

    public IEnumerable<string> GetFileContentList()
    {
        using var fileStream = File.Open(filePath, FileMode.Open);
        if (fileStream == null)
            yield break;

        var buffer = new byte[fileStream.Length];
        var readCount = fileStream.Read(buffer, 0, buffer.Length);
        if (buffer.Length != readCount)
            yield break;

        DetectionResult charset = CharsetDetector.DetectFromBytes(buffer);
        string result = charset.Detected.Encoding.GetString(buffer);

        var stringStream = new StringReader(result);
        while (true)
        {
            var line = stringStream.ReadLine();
            if (line == null)
                yield break;
            if (string.IsNullOrEmpty(line))
                continue;
            yield return line; 
        }
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
