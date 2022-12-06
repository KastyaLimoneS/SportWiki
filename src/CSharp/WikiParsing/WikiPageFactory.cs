using System;
using System.Net;
using System.IO;

namespace WikiParsing;

internal static class WikiPageFactory
{
    public static WikiPage GetPageFrom(String url)
    {
        var objStream = WebRequest.Create("https://en.wikipedia.org/wiki/CD_Pozoblanco").GetResponse().GetResponseStream();
        StreamReader objReader = new StreamReader(objStream);

        string sLine = "";
        int i = 0;

        while (sLine != null)
        {
            i++;
            sLine = objReader.ReadLine();
            if (sLine != null)
                Output.WriteLine(String.Format("{0}:{1}", i, sLine));
        }      
        return null;
    }
}
