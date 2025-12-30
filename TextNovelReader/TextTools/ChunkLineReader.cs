using System.Text;

namespace TextNovelReader.TextTools;

public sealed class ChunkLineReader : IAsyncDisposable, IDisposable
{
    private readonly Stream _stream;
    private readonly Decoder _decoder;

    private readonly byte[] _byteBuffer;
    private readonly char[] _charBuffer;

    private StringBuilder _lineBuilder = new();

    private int _charIndex;
    private int _charCount;
    private bool _endOfStream;

    public ChunkLineReader(string filePath, int bufferSize = 4096)
    {
        var encoding = TextDecoder.GetEncoding(filePath, 100);
        _decoder = encoding.GetDecoder(); 

        _stream = new FileStream(filePath, FileMode.Open);
        _byteBuffer = new byte[bufferSize];
        _charBuffer = new char[encoding.GetMaxCharCount(bufferSize)];
    }

    public void Dispose()
    {
        _stream.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _stream.DisposeAsync();
    }

    public async ValueTask<string?> ReadLineAsync()
    {
        if (_endOfStream)
            return null;

        while (true)
        {
            if (_charIndex < _charCount)
            {
                char c = _charBuffer[_charIndex++];

                if (c == '\n')
                {
                    string line = _lineBuilder.ToString();
                    _lineBuilder = _lineBuilder.Clear();
                    return line;
                }

                if (c != '\r')
                    _lineBuilder.Append(c);

                continue;
            }

            int bytesRead = await _stream.ReadAsync(_byteBuffer);
            if (bytesRead == 0)
            {
                _charCount = _decoder.GetChars([], 0, 0, _charBuffer, 0, flush: true);
                _charIndex = 0;
                _endOfStream = true;

                if (_charCount > 0)
                    continue;

                if (_lineBuilder.Length > 0)
                {
                    string lastLine = _lineBuilder.ToString();
                    _lineBuilder.Clear();
                    return lastLine;
                }

                return null; 
            }

            _charCount = _decoder.GetChars(_byteBuffer, 0, bytesRead, _charBuffer, 0, flush: false);
            _charIndex = 0;
        }
    }
}
