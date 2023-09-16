namespace TaleLearnCode.SonarCloudNet.UnitTests.Authentication;

public class Validate
{

  private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
  private readonly SonarCloudClient _sut;

  public Validate()
  {
    _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
    _sut = new SonarCloudClient(new HttpClient(_mockHttpMessageHandler.Object), "BearerToken");
  }

  [Fact]
  public async Task NullBearerToken_ThrowsArgumentNullException()
  {

    // Arrange

    // Act
    async Task Act() => await _sut.Validate(null);

    // Assert
    await Assert.ThrowsAsync<ArgumentNullException>(Act);

  }

  [Fact]
  public async Task EmptyBearerToken_ThrowsArgumentNullException()
  {

    // Arrange

    // Act
    async Task Act() => await _sut.Validate(string.Empty);

    // Assert
    await Assert.ThrowsAsync<ArgumentNullException>(Act);

  }

  [Fact]
  public async Task WhitespaceBearerToken_ThrowsArgumentNullException()
  {

    // Arrange

    // Act
    async Task Act() => await _sut.Validate(" ");

    // Assert
    await Assert.ThrowsAsync<ArgumentNullException>(Act);

  }

  [Fact]
  public async Task ValidBearerToken_ReturnsTrue()
  {

    // Arrange
    bool expected = true;
    HttpResponseMessage message = new()
    {
      Content = new StringContent(@"{""valid"":true}")
    };
    _mockHttpMessageHandler.Protected()
      .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
      .ReturnsAsync(message);

    // Act
    bool actual = await _sut.Validate("BearerToken");

    // Assert
    Assert.Equal(expected, actual);

  }

  [Fact]
  public async Task InvalidBearerToken_ReturnsFalse()
  {

    // Arrange
    bool expected = false;
    HttpResponseMessage message = new HttpResponseMessage();
    message.Content = new StringContent(@"{""valid"":false}");
    _mockHttpMessageHandler.Protected().
        Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).
        ReturnsAsync(message);

    // Act
    bool actual = await _sut.Validate("BearerToken");

    // Assert
    Assert.Equal(expected, actual);

  }

  [Fact]
  public async Task WithoutBearerToken_ValidBearerToken_ReturnsTrue()
  {

    // Arrange
    bool expected = true;
    HttpResponseMessage message = new HttpResponseMessage();
    message.Content = new StringContent(@"{""valid"":true}");
    _mockHttpMessageHandler.Protected().
        Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).
        ReturnsAsync(message);

    // Act
    bool actual = await _sut.Validate();

    // Assert
    Assert.Equal(expected, actual);

  }

  [Fact]
  public async Task WithoutBearerToken_InvalidBearerToken_ReturnsFalse()
  {

    // Arrange
    bool expected = false;
    HttpResponseMessage message = new HttpResponseMessage();
    message.Content = new StringContent(@"{""valid"":false}");
    _mockHttpMessageHandler.Protected().
        Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).
        ReturnsAsync(message);

    // Act
    bool actual = await _sut.Validate();

    // Assert
    Assert.Equal(expected, actual);

  }


}