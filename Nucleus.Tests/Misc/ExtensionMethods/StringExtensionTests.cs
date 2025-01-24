using System.Text;
using Nucleus.Misc.ExtensionMethods;

namespace Nucleus.Tests.Misc.ExtensionMethods;

public class StringExtensionTests
{
    private const string Input = "Hello, World!";

    [Fact]
    public void AsByteArray_ShouldConvertStringToByteArray()
    {
        // Arrange
        var expected = Encoding.UTF8.GetBytes(Input);
        // Act
        var actual = Input.AsByteArray();
        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AsStream_ShouldConvertStringToStream()
    {
        // Arrange
        var expected = new MemoryStream(Encoding.UTF8.GetBytes(Input));
        // Act
        var actual = Input.AsStream() as MemoryStream;
        // Assert
        Assert.Equal(expected.ToArray(), actual?.ToArray());
    }

    [Fact]
    public void AsStream_ShouldConvertStringToStreamAndRead()
    {
        // Arrange
        var expected = new MemoryStream(Encoding.UTF8.GetBytes(Input));
        // Act
        var actual = Input.AsStream();
        // Assert
        Assert.Equal(expected.ToArray(), actual.AsByteArray());
    }
}