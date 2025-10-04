using System.Diagnostics.CodeAnalysis;
using Gucu112.CSharp.Automation.Helpers.Models;

namespace Gucu112.CSharp.Automation.Helpers.Parsers;

/// <inheritdoc path="Common/Helpers/Parsers/Parse.cs"/>
[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1648:inheritdoc should be used with inheriting class",
    Justification = "partial class")]
public static partial class Parse
{
    private static readonly JsonParse JsonParse = new();

    /// <inheritdoc cref="IJsonParse.FromJson{TOutput}(string, JsonSettings?)"/>
    public static TOutput? FromJson<TOutput>(string content, JsonSettings? settings = null)
    {
        return JsonParse.FromJson<TOutput>(content, settings);
    }

    /// <inheritdoc cref="IJsonParse.FromJson{TOutput}(TextReader, JsonSettings?)"/>
    public static TOutput? FromJson<TOutput>(TextReader textReader, JsonSettings? settings = null)
    {
        return JsonParse.FromJson<TOutput>(textReader, settings);
    }

    /// <inheritdoc cref="IJsonParse.FromJson{TOutput}(Stream, JsonSettings?)"/>
    public static TOutput? FromJson<TOutput>(Stream stream, JsonSettings? settings = null)
    {
        return JsonParse.FromJson<TOutput>(stream, settings);
    }

    /// <inheritdoc cref="IJsonParse.FromJsonFile{TOutput}(string, JsonSettings?)"/>
    public static TOutput? FromJsonFile<TOutput>(string path, JsonSettings? settings = null)
    {
        return JsonParse.FromJsonFile<TOutput>(path, settings);
    }

    /// <inheritdoc cref="IJsonParse.ToJsonString(object?, JsonSettings?)"/>
    public static string ToJsonString(object? value, JsonSettings? settings = null)
    {
        return JsonParse.ToJsonString(value, settings);
    }

    /// <inheritdoc cref="IJsonParse.ToJsonWriter{TWriter}(object?, JsonSettings?, TWriter)"/>
    public static TWriter ToJsonWriter<TWriter>(object? value, JsonSettings? settings = null, TWriter? textWriter = null)
        where TWriter : TextWriter
    {
        return JsonParse.ToJsonWriter(value, settings, textWriter);
    }

    /// <inheritdoc cref="IJsonParse.ToJsonStream{TStream}(object?, JsonSettings?, TStream)"/>
    public static TStream ToJsonStream<TStream>(object? value, JsonSettings? settings = null, TStream? stream = null)
        where TStream : Stream
    {
        return JsonParse.ToJsonStream(value, settings, stream);
    }

    /// <inheritdoc cref="IJsonParse.ToJsonFile(object?, string, JsonSettings?)"/>
    public static void ToJsonFile(object? value, string path, JsonSettings? settings = null)
    {
        JsonParse.ToJsonFile(value, path, settings);
    }
}
