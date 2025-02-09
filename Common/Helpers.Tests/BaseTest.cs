namespace Gucu112.CSharp.Automation.Helpers.Tests;

/// <summary>
/// Base class for unit tests.
/// </summary>
public class BaseTest
{
    private byte[] streamData = [];

    /// <summary>
    /// Gets a mock of <see cref="MemoryStream"/> with the ability to write and store data.
    /// </summary>
    /// <returns>The mocked <see cref="MemoryStream"/>.</returns>
    protected Mock<MemoryStream> GetMemoryStreamMock()
    {
        var streamMock = new Mock<MemoryStream>() { CallBase = true };

        void StreamWriteCallback(byte[] buffer, int offset, int count)
        {
            var bytes = offset > streamData.Length
                ? streamData.Concat(Enumerable.Repeat<byte>(0, offset - streamData.Length))
                : streamData[0..offset];

            streamData = bytes.Concat(buffer.Take(count)).ToArray();
        }

        streamMock.Setup(ms => ms.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
            .Callback(StreamWriteCallback).CallBase();

        return streamMock;
    }

    /// <summary>
    /// Gets the data from the mocked <see cref="MemoryStream"/> as a string.
    /// </summary>
    /// <param name="encoding">The encoding to use for decoding the data. If null, <see cref="Encoding.UTF8"/> will be used.</param>
    /// <returns>The data retrieved from the mocked <see cref="MemoryStream"/>.</returns>
    protected string GetMemoryStreamData(Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return encoding.GetString(streamData, 0, streamData.Length);
    }
}
