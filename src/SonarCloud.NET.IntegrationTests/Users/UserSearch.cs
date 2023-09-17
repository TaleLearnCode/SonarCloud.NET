namespace TaleLearnCode.SonarCloudNet.IntegrationsTests.Users;

public class UserSearch : IClassFixture<TestFixture>
{

  private readonly TestFixture _fixture;

  private const int _pageSize = 50;

  public UserSearch(TestFixture fixture) => _fixture = fixture;

  [Fact]
  public async Task TestMe()
  {
    UserSearchResponse? response = await _fixture.SonarCloudClient.UserSearch(_fixture.BearerToken);
    Assert.NotNull(response);
  }



  [Fact]
  public async Task PageSizeGreaterThen500_ThrowsArgumentException()
  {

    // Arrange
    int pageSize = 501;

    // Act
    async Task Act() => await _fixture.SonarCloudClient.UserSearch(bearerToken: _fixture.BearerToken, pageSize: pageSize);

    // Assert
    await Assert.ThrowsAsync<ArgumentException>(Act);

  }

  [Fact]
  public async Task QueryLessThenTwoCharacters_ThrowsArgumentException()
  {

    // Arrange
    string query = "a";

    // Act
    async Task Act() => await _fixture.SonarCloudClient.UserSearch(bearerToken: _fixture.BearerToken, query: query);

    // Assert
    await Assert.ThrowsAsync<ArgumentException>(Act);

  }

  [Fact]
  public async Task InvalidBearerToken_ThrowUnauthorizedException()
  {

    // Arrange
    string bearerToken = "BadToken";

    // Act
    async Task Act() => await _fixture.SonarCloudClient.UserSearch(bearerToken: bearerToken);

    // Assert
    await Assert.ThrowsAsync<UnauthorizedException>(Act);

  }

  [Fact]
  public async Task NoParameters_ReturnsUserSearchResponse()
  {

    // Arrange
    UserSearchResponse expected = GetExpectedResponse();

    // Act
    UserSearchResponse? actual = await _fixture.SonarCloudClient.UserSearch(bearerToken: _fixture.BearerToken);

    // Assert
    Assert.NotNull(actual);
    AssertPagingInfo(expected, actual);
    AssertUserCount(_pageSize, actual);
  }

  [Fact]
  public async Task WithPageSize_ReturnsUserSearchResponse()
  {

    // Arrange
    int pageSize = 5;
    UserSearchResponse expected = GetExpectedResponse(pageSize: pageSize);

    // Act
    UserSearchResponse? actual = await _fixture.SonarCloudClient.UserSearch(bearerToken: _fixture.BearerToken, pageSize: pageSize);

    // Assert
    Assert.NotNull(actual);
    AssertPagingInfo(expected, actual);
    AssertUserCount(pageSize, actual);
  }

  [Fact]
  public async Task WithPageIndex_ReturnsUserSearchResponse()
  {

    // Arrange
    int pageIndex = 5;
    UserSearchResponse expected = GetExpectedResponse(pageIndex: pageIndex);

    // Act
    UserSearchResponse? actual = await _fixture.SonarCloudClient.UserSearch(bearerToken: _fixture.BearerToken, pageIndex: pageIndex);

    // Assert
    Assert.NotNull(actual);
    AssertPagingInfo(expected, actual);
    AssertUserCount(_pageSize, actual);
  }

  [Fact]
  public async Task WithQuery_ReturnsUserSearchResponse()
  {

    // Arrange
    UserSearchResponse expected = GetExpectedResponse(total: 1);

    // Act
    UserSearchResponse? actual = await _fixture.SonarCloudClient.UserSearch(bearerToken: _fixture.BearerToken, query: "TaleLearnCode@github");

    // Assert
    Assert.NotNull(actual);
    AssertPagingInfo(expected, actual);
    AssertUserCount(1, actual);
    AssertUserDetails(expected, actual);

  }

  private static UserSearchResponse GetExpectedResponse(int pageIndex = 1, int pageSize = 50, int total = 10000)
  {
    return new()
    {
      Paging = new()
      {
        PageIndex = pageIndex,
        PageSize = pageSize,
        Total = total
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
  }

  private static void AssertPagingInfo(UserSearchResponse expected, UserSearchResponse? actual)
  {
    if (actual is not null)
    {
      Assert.Equal(expected.Paging.PageIndex, actual.Paging.PageIndex);
      Assert.Equal(expected.Paging.PageSize, actual.Paging.PageSize);
      Assert.Equal(expected.Paging.Total, actual.Paging.Total);
    }
  }

  private static void AssertUserCount(int expectedUserCount, UserSearchResponse? actual)
  {
    if (actual is not null)
    {
      Assert.NotNull(actual.Users);
      Assert.Equal(expectedUserCount, actual.Users.Count);
    }
  }

  private static void AssertUserDetails(UserSearchResponse expected, UserSearchResponse? actual)
  {
    if (actual is not null)
    {
      Assert.Equal(expected.Users[0].Login, actual.Users[0].Login);
      Assert.Equal(expected.Users[0].Name, actual.Users[0].Name);
      Assert.Equal(expected.Users[0].Active, actual.Users[0].Active);
      Assert.Equal(expected.Users[0].Local, actual.Users[0].Local);
      Assert.Equal(expected.Users[0].ExternalProvider, actual.Users[0].ExternalProvider);
      Assert.Equal(expected.Users[0].Avatar, actual.Users[0].Avatar);
    }
  }

}