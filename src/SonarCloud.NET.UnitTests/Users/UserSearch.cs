using TaleLearnCode.SonarCloudNet.Responses;
using TaleLearnCode.SonarCloudNet.Responses.Components;

#nullable disable

namespace TaleLearnCode.SonarCloudNet.UnitTests.Users;

public class UserSearchTests : IClassFixture<TestFixture>
{

  private readonly TestFixture _fixture;

  public UserSearchTests(TestFixture fixture)
  {
    _fixture = fixture;
  }

  [Fact]
  public async Task NullBearerToken_ThrowsArgumentNullException() => await Token_ThrowsArgumentNullException(null);

  [Fact]
  public async Task EmptyBearerToken_ThrowsArgumentNullException() => await Token_ThrowsArgumentNullException(string.Empty);

  [Fact]
  public async Task WhitespaceBearerToken_ThrowsArgumentNullException() => await Token_ThrowsArgumentNullException(" ");

  [Fact]
  public async Task PageSizeGreaterThen500_ThrowsArgumentException()
  {

    // Arrange
    int pageSize = 501;

    // Act
    async Task Act() => await _fixture.SonarCloudClient.UserSearch(pageSize: pageSize);

    // Assert
    await Assert.ThrowsAsync<ArgumentException>(Act);

  }

  [Fact]
  public async Task QueryLessThenTwoCharacters_ThrowsArgumentException()
  {

    // Arrange
    string query = "a";

    // Act
    async Task Act() => await _fixture.SonarCloudClient.UserSearch(query: query);

    // Assert
    await Assert.ThrowsAsync<ArgumentException>(Act);

  }

  [Fact]
  public async Task ReturnsUserSearchResponse()
  {

    // Arrange
    UserSearchResponse expected = new()
    {
      Paging = new()
      {
        PageIndex = 1,
        PageSize = 50,
        Total = 1
      },
      Users = new List<UserResponse>()
      {
        new()
        {
          Login = "TaleLearnCode@github",
          Name = "Tale Learn Code",
          Active = true,
          Local = false,
          ExternalProvider = "github",
          Avatar = "47aa5a0f4d66b63a1cdc7029172a8ecc"
        }
      }
    };

    HttpResponseMessage httpResponseMessage = _fixture.GenerateHttpResponseMessage(expected);

    _fixture.MockedHttpMessageHandler.Protected()
      .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
      .ReturnsAsync(httpResponseMessage);

    // Act
    UserSearchResponse actual = await _fixture.SonarCloudClient.UserSearch();

    // Assert
    Assert.NotNull(actual);
    Assert.Equal(expected.Paging.PageIndex, actual.Paging.PageIndex);
    Assert.Equal(expected.Paging.PageSize, actual.Paging.PageSize);
    Assert.Equal(expected.Paging.Total, actual.Paging.Total);
    Assert.Equal(expected.Users.Count, actual.Users.Count);
    Assert.Equal(expected.Users[0].Login, actual.Users[0].Login);
    Assert.Equal(expected.Users[0].Name, actual.Users[0].Name);
    Assert.Equal(expected.Users[0].Active, actual.Users[0].Active);
    Assert.Equal(expected.Users[0].Local, actual.Users[0].Local);
    Assert.Equal(expected.Users[0].ExternalProvider, actual.Users[0].ExternalProvider);
    Assert.Equal(expected.Users[0].Avatar, actual.Users[0].Avatar);

  }

  private async Task Token_ThrowsArgumentNullException(string bearerToken)
  {
    async Task Act() => await _fixture.SonarCloudClient.UserSearch(bearerToken);
    await Assert.ThrowsAsync<ArgumentNullException>(Act);
  }

}