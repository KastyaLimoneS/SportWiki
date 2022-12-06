using System;
using System.IO;

namespace WikiParsing;

public class Output
{
    public static void WriteLine(String line)
    {
        using (var writer = new StreamWriter("../../Output.html", true))
        {
            writer.Write(DeleteFuckingNumbers(line));
        }
    }

    private static String DeleteFuckingNumbers(String line)
    {
        var found = false;
        var result = "";
        foreach (var el in line)
        {
            if (found) {
                result += el;
                continue;
            }
            if (!found && el == ':') found = true;
        }
        return result;
    }


}
