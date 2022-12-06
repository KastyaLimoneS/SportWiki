using System;
using System.Net;
using System.IO;

namespace WikiParsing;

internal static class WikiPageFactory
{
    private static readonly String ID_STRING_KEY = "\"wgWikibaseItemId\":";
    public static WikiPage GetPageFrom(String url)
    {
        var objStream = WebRequest.Create(url).GetResponse().GetResponseStream();
        StreamReader objReader = new StreamReader(objStream);

        string sLine = "";
        int i = 0;
        while (sLine != null)
        {
            i++;
            sLine = objReader.ReadLine();
            if (sLine != null)
            {
                var id = ExtractItemId(sLine);
                if (id != null) return new WikiPage(id);
            }
        }      
        return null;
    }

    private static String? ExtractItemId(String line)
    {
        int index = line.IndexOf(ID_STRING_KEY);
        if (index != -1)
        {
            var result = "";
            foreach (var el in line.Substring(index+ID_STRING_KEY.Length))
            {
                if (el == ',') return result.Trim('"');
                result += el;
                continue;
            }
        }
        return null;
    }
}
