namespace TaleLearnCode.SonarCloudNet.UnitTests;

public class SonarCloudClientTests
{

  private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;

  public SonarCloudClientTests()
  {
    _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
  }


  [Fact]
  public void Constructor_NoParameters_Initializes()
  {

    // Arrange

    // Act
    using SonarCloudClient sonarCloudClient = new SonarCloudClient();

    // Assert
    Assert.IsType<SonarCloudClient>(sonarCloudClient);

  }

  [Fact]
  public void Constructor_HttpClient_Initializes()
  {

    // Arrange

    // Act
    using SonarCloudClient sonarCloudClient = new(new HttpClient(_mockHttpMessageHandler.Object));

    // Assert
    Assert.IsType<SonarCloudClient>(sonarCloudClient);

  }

  [Fact]
  public void Constructor_BearerToken_Initializes()
  {

    // Arrange

    // Act
    using SonarCloudClient sonarCloudClient = new("BearerToken");

    // Assert
    Assert.IsType<SonarCloudClient>(sonarCloudClient);

  }

  [Fact]
  public void Constructor_HttpClientAndBearerToken_Initializes()
  {

    // Arrange

    // Act
    using SonarCloudClient sonarCloudClient = new(new HttpClient(_mockHttpMessageHandler.Object), "BearerToken");

    // Assert
    Assert.IsType<SonarCloudClient>(sonarCloudClient);

  }

  [Fact]
  public void BaseApiAddress_Get_ReturnsExpectedValue()
  {

    // Arrange
    string expectedValue = "https://sonarcloud.io/api/";
    using SonarCloudClient sonarCloudClient = new SonarCloudClient();

    // Act
    string result = sonarCloudClient.BaseApiAddress;

    // Assert
    Assert.Equal(expectedValue, result);

  }

}