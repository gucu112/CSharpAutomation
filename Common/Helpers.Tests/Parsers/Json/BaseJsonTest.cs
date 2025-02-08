using Allure.NUnit;
using Gucu112.CSharp.Automation.Helpers.Models;
using Gucu112.CSharp.Automation.Helpers.Parsers;

namespace Gucu112.CSharp.Automation.Helpers.Tests.Parsers.Json;

[AllureNUnit]
public class BaseJsonTest
{
    private byte[] streamData = [];

    protected static T? ParseFromJson<T>(dynamic? input, JsonSettings? settings = null)
        where T : class
    {
        return Parse.FromJson<T>(input, settings);
    }

    protected static string ParseToJson<T>(object? value, JsonSettings? settings = null)
    {
        if (typeof(T) == typeof(Stream))
        {
            var encoding = settings?.Encoding ?? Encoding.UTF8;
            using var stream = Parse.ToJsonStream<MemoryStream>(value, settings);
            return encoding.GetString(stream.GetBuffer(), 0, (int)stream.Length);
        }

        if (typeof(T) == typeof(TextWriter))
        {
            using var writer = Parse.ToJsonWriter<StringWriter>(value, settings);
            return writer.GetStringBuilder().ToString();
        }

        return Parse.ToJsonString(value, settings);
    }

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

    protected string GetMemoryStreamData(Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return encoding.GetString(streamData, 0, streamData.Length);
    }
}
