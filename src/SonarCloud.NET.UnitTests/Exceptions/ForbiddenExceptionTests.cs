namespace TaleLearnCode.SonarCloudNet.UnitTests.Exceptions;

public class ForbiddenExceptionTests
{

  private const string _defaultMessage = "The SonarCloud Web API request has not been completed because the user has insufficient privileges.";

  [Fact]
  public void Constructor_Default_Exception()
  {
    // Arrange & Act
    var ex = new ForbiddenException();

    // Assert
    Assert.Equal(_defaultMessage, ex.Message);
  }

  [Fact]
  public void Constructor_String_Exception()
  {
    // Arrange & Act
    var ex = new ForbiddenException("Custom exception message");

    // Assert
    Assert.Equal("Custom exception message", ex.Message);
  }

  [Fact]
  public void Constructor_StringAndException_Exception()
  {
    // Arrange
    var innerException = new Exception("Inner exception message");

    // Act
    var ex = new ForbiddenException("Custom exception message", innerException);

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
    var ex = new ForbiddenException(innerException);

    // Assert
    Assert.Equal(_defaultMessage, ex.Message);
    Assert.Equal(innerException, ex.InnerException);
  }

}