using System.Text;
using Nucleus.Misc.ExtensionMethods;

namespace Nucleus.Tests.Misc.ExtensionMethods;

public class StringExtensionTests
{
    [Fact]
    public void AsByteArray_ShouldConvertStringToByteArray()
    {
        // Arrange
        const string input = "Hello, World!";
        var expected = Encoding.UTF8.GetBytes(input);
        // Act
        var actual = input.AsByteArray();
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AsStream_ShouldConvertStringToStream()
    {
        // Arrange
        const string input = "Hello, World!";
        var expected = new MemoryStream(Encoding.UTF8.GetBytes(input));
        // Act
        var actual = input.AsStream() as MemoryStream;
        // Assert
        Assert.Equal(expected.ToArray(), actual?.ToArray());
    }

    [Fact]
    public void AsStream_ShouldConvertStringToStreamAndRead()
    {
        // Arrange
        const string input = "Hello, World!";
        var expected = new MemoryStream(Encoding.UTF8.GetBytes(input));
        // Act
        var actual = input.AsStream();
        // Assert
        Assert.Equal(expected.ToArray(), actual.AsByteArray());
    }
}