using System;
using System.Net;
using System.Linq;
using System.Collections;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace WikiParsing;

internal static class WikiPageFactory
{
    private static readonly String REQUEST_INTERNAL_PAGES = "https://ru.wikipedia.org/w/api.php?action=query&format=json&prop=info&titles={0}&generator=links&formatversion=2&inprop=url%7Csubjectid&gplnamespace=0&gpllimit=500";
    private static readonly String REQUEST_PAGE_TEXT = "https://ru.wikipedia.org/w/api.php?action=parse&format=json&page={0}&prop=text&formatversion=2";
    public static void PrintInternalPages(String title)
    {
        Console.WriteLine(title);
        var doc = GetResponseJson(REQUEST_INTERNAL_PAGES, title);
        var pages = doc.RootElement.GetProperty("query").GetProperty("pages").EnumerateArray();
        foreach (var el in pages)
            Console.WriteLine(String.Format("-{0}", el.GetProperty("title").GetString()));
        
    }

    public static String GetUnparsedPageText(String title)
    {
        var doc = GetResponseJson(REQUEST_PAGE_TEXT, title);
        return doc.RootElement.GetProperty("parse").GetProperty("text").GetString();
    }

    public static JsonDocument GetResponseJson(String title, String requestTemplate)
    {
        var objStream = WebRequest.Create(String.Format(requestTemplate, title)).GetResponse().GetResponseStream();
        var objReader = new LinqStreamReader(objStream);
        return JsonDocument.Parse(objReader.Aggregate((a, b) => a+'\n'+b));
    }
}

internal class LinqStreamReader : StreamReader, IEnumerable<string>
{
    public LinqStreamReader(Stream stream) : base(stream)
    {}

    public LinqStreamReader(string path) : base(path)
    {}

    public LinqStreamReader(Stream stream, bool detectEncodingFromByteOrderMarks) : base(stream, detectEncodingFromByteOrderMarks)
    {}

    public LinqStreamReader(Stream stream, Encoding encoding) : base(stream, encoding)
    {}

    public LinqStreamReader(string path, bool detectEncodingFromByteOrderMarks) : base(path, detectEncodingFromByteOrderMarks)
    {}

    public LinqStreamReader(string path, FileStreamOptions options) : base(path, options)
    {}

    public LinqStreamReader(string path, Encoding encoding) : base(path, encoding)
    {}

    public LinqStreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) : base(stream, encoding, detectEncodingFromByteOrderMarks)
    {}

    public LinqStreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks) : base(path, encoding, detectEncodingFromByteOrderMarks)
    {}

    public LinqStreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize)
    {}

    public LinqStreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) : base(path, encoding, detectEncodingFromByteOrderMarks, bufferSize)
    {}

    public LinqStreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, FileStreamOptions options) : base(path, encoding, detectEncodingFromByteOrderMarks, options)
    {}

    public LinqStreamReader(Stream stream, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = -1, bool leaveOpen = false) : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen)
    {}

    public IEnumerator<String> GetEnumerator()
    {
        return new LinqStreamReaderEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

internal class LinqStreamReaderEnumerator : IEnumerator<String>
{
    private LinqStreamReader _reader;
    public LinqStreamReaderEnumerator(LinqStreamReader reader) => _reader = reader;
    public string _current;

    string IEnumerator<string>.Current => _current;

    object IEnumerator.Current => _current;

    public bool MoveNext()
    {
        var line = _reader.ReadLine();
        if (line == null) return false;
        _current = line;
        return true;
    }

    public void Reset()
    {}

    public void Dispose()
    {}
}
