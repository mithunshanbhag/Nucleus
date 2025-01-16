namespace Nucleus.Tests.Repositories;

public class TestDao
{
    public string? Id { get; set; }
}

public interface ICosmosTestRepository : ICosmosGenericRepository<TestDao>
{
}

public class CosmosTestRepository(Database cosmosDatabase)
    : CosmosGenericRepositoryBase<TestDao>(cosmosDatabase, "testContainer"), ICosmosTestRepository;

public class CosmosGenericRepositoryTests
{
    private readonly Mock<Container> _containerMock = new();
    private readonly Mock<Database> _databaseMock = new();

    private readonly ICosmosTestRepository _sut;

    public CosmosGenericRepositoryTests()
    {
        _databaseMock
            .Setup(db => db.GetContainer(It.IsAny<string>()))
            .Returns(_containerMock.Object);

        _sut = new CosmosTestRepository(_databaseMock.Object);
    }

    [Fact]
    public async Task AddAsync_ValidRequest_Returns()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var partitionKey = "Temperature";
        var entity = new TestDao { Id = timestamp.ToString("o") };
        var cancellationToken = CancellationToken.None;

        // Act
        await _sut.AddAsync(partitionKey, entity, cancellationToken);

        // Assert
        _containerMock.Verify(
            c => c.CreateItemAsync(entity, new PartitionKey(partitionKey), null, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task UpsertAsync_ValidRequest_Returns()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var partitionKey = "Temperature";
        var entity = new TestDao { Id = timestamp.ToString("o") };
        var cancellationToken = CancellationToken.None;

        // Act
        await _sut.UpsertAsync(partitionKey, entity, cancellationToken);

        // Assert
        _containerMock.Verify(
            c => c.UpsertItemAsync(entity, new PartitionKey(partitionKey), null, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ValidRequest_Returns()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var partitionKey = "Temperature";
        var entity = new TestDao { Id = timestamp.ToString("o") };
        var cancellationToken = CancellationToken.None;

        // Act
        await _sut.DeleteAsync(partitionKey, entity.Id, cancellationToken);

        // Assert
        _containerMock.Verify(
            c => c.DeleteItemAsync<TestDao>(entity.Id, new PartitionKey(partitionKey), null, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetAsync_ValidRequest_Returns()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var partitionKey = "Temperature";
        var entity = new TestDao { Id = timestamp.ToString("o") };
        var cancellationToken = CancellationToken.None;

        // Act
        await _sut.GetAsync(partitionKey, entity.Id, cancellationToken);

        // Assert
        _containerMock.Verify(
            c => c.ReadItemAsync<TestDao>(entity.Id, new PartitionKey(partitionKey), null, cancellationToken), Times.Once);
    }

    #region Cancellation Tests

    [Fact]
    public async Task AddAsync_CancellationRequest_ThrowsException()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var partitionKey = "Temperature";
        var entity = new TestDao { Id = timestamp.ToString("o") };
        var cancellationToken = new CancellationToken(true);

        // Act
        var func = async () => await _sut.AddAsync(partitionKey, entity, cancellationToken);

        // Assert
        await Assert.ThrowsAsync<OperationCanceledException>(func);
    }

    [Fact]
    public async Task UpsertAsync_CancellationRequest_ThrowsException()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var partitionKey = "Temperature";
        var entity = new TestDao { Id = timestamp.ToString("o") };
        var cancellationToken = new CancellationToken(true);

        // Act
        var func = async () => await _sut.UpsertAsync(partitionKey, entity, cancellationToken);

        // Assert
        await Assert.ThrowsAsync<OperationCanceledException>(func);
    }

    [Fact]
    public async Task DeleteAsync_CancellationRequest_ThrowsException()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var partitionKey = "Temperature";
        var entity = new TestDao { Id = timestamp.ToString("o") };
        var cancellationToken = new CancellationToken(true);

        // Act
        var func = async () => await _sut.DeleteAsync(partitionKey, entity.Id, cancellationToken);

        // Assert
        await Assert.ThrowsAsync<OperationCanceledException>(func);
    }

    [Fact]
    public async Task GetAsync_CancellationRequest_ThrowsException()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var partitionKey = "Temperature";
        var entity = new TestDao { Id = timestamp.ToString("o") };
        var cancellationToken = new CancellationToken(true);

        // Act
        var func = async () => await _sut.GetAsync(partitionKey, entity.Id, cancellationToken);

        // Assert
        await Assert.ThrowsAsync<OperationCanceledException>(func);
    }

    #endregion
}
