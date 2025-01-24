namespace Nucleus.Misc.ExtensionMethods;

public static class ByteArrayExtension
{
    public static string AsString(this byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }

    public static Stream AsStream(this byte[] bytes)
    {
        return new MemoryStream(bytes);
    }
}