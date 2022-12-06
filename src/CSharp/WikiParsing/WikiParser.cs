namespace WikiParsing;

public class WikiParser
{
    public void DoSmth()
    {
        Console.WriteLine(WikiPageFactory.GetPageFrom("dm").Id);
    }
}
