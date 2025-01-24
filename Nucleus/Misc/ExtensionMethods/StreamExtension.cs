namespace Nucleus.Misc.ExtensionMethods;

public static class StreamExtension
{
    public static byte[] AsByteArray(this Stream stream)
    {
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    public static string AsString(this Stream stream)
    {
        return Encoding.UTF8.GetString(stream.AsByteArray());
    }
}