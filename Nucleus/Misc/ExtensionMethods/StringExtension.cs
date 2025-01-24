namespace Nucleus.Misc.ExtensionMethods;

public static class StringExtension
{
    public static byte[] AsByteArray(this string input)
    {
        return Encoding.UTF8.GetBytes(input);
    }

    public static Stream AsStream(this string input)
    {
        return new MemoryStream(input.AsByteArray());
    }
}