using Gucu112.CSharp.Automation.Helpers.Models;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

#pragma warning disable SA1648 // InheritDocMustBeUsedWithInheritingClass
/// <inheritdoc path="Common/Helpers/Parsers/Parse.cs"/>
#pragma warning restore SA1648 // InheritDocMustBeUsedWithInheritingClass
public static partial class Parse
{
    private static readonly JsonParse JsonParse = new();

    /// <inheritdoc cref="IJsonParse.FromJson{T}(string, JsonSettings?)"/>
    public static T? FromJson<T>(string content, JsonSettings? settings = null)
    {
        return JsonParse.FromJson<T>(content, settings);
    }

    /// <inheritdoc cref="IJsonParse.FromJson{T}(TextReader, JsonSettings?)"/>
    public static T? FromJson<T>(TextReader textReader, JsonSettings? settings = null)
    {
        return JsonParse.FromJson<T>(textReader, settings);
    }

    /// <inheritdoc cref="IJsonParse.FromJson{T}(Stream, JsonSettings?)"/>
    public static T? FromJson<T>(Stream stream, JsonSettings? settings = null)
    {
        return JsonParse.FromJson<T>(stream, settings);
    }

    /// <inheritdoc cref="IJsonParse.FromJsonFile{T}(string, JsonSettings?)"/>
    public static T? FromJsonFile<T>(string path, JsonSettings? settings = null)
    {
        return JsonParse.FromJsonFile<T>(path, settings);
    }

    /// <inheritdoc cref="IJsonParse.ToJsonString(object?, JsonSettings?)"/>
    public static string ToJsonString(object? value, JsonSettings? settings = null)
    {
        return JsonParse.ToJsonString(value, settings);
    }

    /// <inheritdoc cref="IJsonParse.ToJsonWriter{T}(object?, JsonSettings?, T)"/>
    public static T ToJsonWriter<T>(object? value, JsonSettings? settings = null, T? textWriter = null)
        where T : TextWriter
    {
        return JsonParse.ToJsonWriter(value, settings, textWriter);
    }

    /// <inheritdoc cref="IJsonParse.ToJsonStream{T}(object?, JsonSettings?, T)"/>
    public static T ToJsonStream<T>(object? value, JsonSettings? settings = null, T? stream = null)
        where T : Stream
    {
        return JsonParse.ToJsonStream(value, settings, stream);
    }

    /// <inheritdoc cref="IJsonParse.ToJsonFile(object?, string, JsonSettings?)"/>
    public static void ToJsonFile(object? value, string path, JsonSettings? settings = null)
    {
        JsonParse.ToJsonFile(value, path, settings);
    }
}
