namespace WikiParsing;

public class WikiParser
{
    public void DoSmth()
    {
        Console.WriteLine(WikiPageFactory.GetPageFrom("https://en.wikipedia.org/wiki/CD_Pozoblanco").Id);
    }
}
