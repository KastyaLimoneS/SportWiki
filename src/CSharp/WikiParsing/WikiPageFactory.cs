using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace WikiParsing;

internal static class WikiPageFactory
{
    private static readonly String REQUEST_FORM = "https://ru.wikipedia.org/w/api.php?action=query&format=json&prop=info&titles={0}&generator=links&formatversion=2&inprop=url%7Csubjectid&gplnamespace=0&gpllimit=10";
    public static void PrintInternalPages(String title)
    {
        var objStream = WebRequest.Create(String.Format(REQUEST_FORM, title)).GetResponse().GetResponseStream();
        var objReader = new LinqStreamReader(objStream);
        Console.WriteLine(objReader.Aggregate((a, b) => a+'\n'+b));
        
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
