namespace TaleLearnCode.SonarCloudNet.UnitTests.Exceptions;

public class TooManyRequestExceptionTests
{

  private const string _defaultMessage = "The SonarCloud Web API request has not been completed because it lacks valid authentication credentials for the requested resource.";

  [Fact]
  public void Constructor_Default_Exception()
  {
    // Arrange & Act
    var ex = new TooManyRequestsException();

    // Assert
    Assert.Equal(_defaultMessage, ex.Message);
  }

  [Fact]
  public void Constructor_String_Exception()
  {
    // Arrange & Act
    var ex = new TooManyRequestsException("Custom exception message");

    // Assert
    Assert.Equal("Custom exception message", ex.Message);
  }

  [Fact]
  public void Constructor_StringAndException_Exception()
  {
    // Arrange
    var innerException = new Exception("Inner exception message");

    // Act
    var ex = new TooManyRequestsException("Custom exception message", innerException);

    // Assert
    Assert.Equal("Custom exception message", ex.Message);
    Assert.Equal(innerException, ex.InnerException);
  }

  [Fact]
  public void Constructor_Exception_Exception()
  {
    // Arrange
    var innerException = new Exception("Inner exception message");

    // Act
    var ex = new TooManyRequestsException(innerException);

    // Assert
    Assert.Equal(_defaultMessage, ex.Message);
    Assert.Equal(innerException, ex.InnerException);
  }

}