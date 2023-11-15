namespace ConsoleApp1;

public static class StringUtil
{
    public static string ToUpperFirst(this string str)
    {
        return str.Substring(0, 1).ToUpper() + str.Substring(1);
    }
}