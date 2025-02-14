using Nucleus.Misc.Helpers;

namespace Nucleus.Tests.Misc.Helpers;

public class Retryable
{
    public int RetryCount { get; set; } = 0;

    public void RetryableMethod()
    {
        RetryCount++;
    }

    public void RetryableMethodWithException()
    {
        RetryCount++;
        throw new Exception();
    }
}

public class RetryHelperTests : IDisposable
{
    private readonly Retryable _retryable;

    public RetryHelperTests()
    {
        _retryable = new Retryable();
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }


    [Fact]
    public void Retry_ShouldRetryMethod()
    {
        // Arrange
        var retryHelper = new RetryHelper();
        // Act
        retryHelper.Execute(_retryable.RetryableMethod);
        // Assert
        Assert.Equal(1, _retryable.RetryCount);
    }

    [Fact]
    public void Retry_ShouldRetryMethodWithException()
    {
        // Arrange
        var retryHelper = new RetryHelper();
        // Act
        retryHelper.Execute(_retryable.RetryableMethodWithException);
        // Assert
        Assert.Equal(4, _retryable.RetryCount);
    }
}