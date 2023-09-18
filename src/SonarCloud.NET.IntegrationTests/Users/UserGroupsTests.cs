namespace TaleLearnCode.SonarCloudNet.IntegrationsTests.Users;

public class UserGroupsTests : IClassFixture<TestFixture>
{

  private readonly TestFixture _fixture;

  public UserGroupsTests(TestFixture fixture) => _fixture = fixture;

  [Fact]
  public async Task PageSizeGreaterThen500_ThrowsArgumentException()
  {

    // Arrange
    int pageSize = 501;

    // Act
    async Task Act() => await _fixture.SonarCloudClient.UserGroups(_fixture.BearerToken, _fixture.OrganizationKey, _fixture.LoginKey, pageSize: pageSize);

    // Assert
    await Assert.ThrowsAsync<ArgumentException>(Act);

  }

  [Fact]
  public async Task InvalidBearerToken_ThrowUnauthorizedException()
  {

    // Arrange
    string bearerToken = "BadToken";

    // Act
    async Task Act() => await _fixture.SonarCloudClient.UserGroups(bearerToken, _fixture.OrganizationKey, _fixture.LoginKey);

    // Assert
    await Assert.ThrowsAsync<UnauthorizedException>(Act);

  }

  [Fact]
  public async Task NonAdminister_ThrowsForbiddenException()
  {

    // Arrange

    // Act
    async Task Act() => await _fixture.SonarCloudClient.UserGroups(_fixture.BearerToken, _fixture.SecondaryOrganizationKey, _fixture.SecondaryLoginKey);

    // Assert
    await Assert.ThrowsAsync<ForbiddenException>(Act);

  }

  [Fact]
  public async Task NoOptionalParameters_ReturnsUserGroupsResponse()
  {

    // Arrange
    UserGroupsResponse expected = GetExpectedResponse();

    // Act
    UserGroupsResponse? actual = await _fixture.SonarCloudClient.UserGroups(_fixture.BearerToken, _fixture.OrganizationKey, _fixture.LoginKey);

    // Assert
    Assert.NotNull(actual);
    AssertPagingInfo(expected, actual);
    AssertGroupCount(2, actual);
    AssertGroupDetails(expected, actual);

  }

  [Fact]
  public async Task WithPageSize_ReturnsUserGroupsResponse()
  {

    // Arrange
    int pageSize = 5;
    UserGroupsResponse expected = GetExpectedResponse(pageSize: pageSize);

    // Act
    UserGroupsResponse? actual = await _fixture.SonarCloudClient.UserGroups(_fixture.BearerToken, _fixture.OrganizationKey, _fixture.LoginKey, pageSize: pageSize);

    // Assert
    Assert.NotNull(actual);
    AssertPagingInfo(expected, actual);
    AssertGroupCount(expected.Groups.Count, actual);
    AssertGroupDetails(expected, actual);

  }

  [Fact]
  public async Task WithPageIndex_ReturnsUserGroupsResponse()
  {

    // Arrange
    int pageIndex = 5;
    UserGroupsResponse expected = GetExpectedNoResultsResponse(pageIndex: pageIndex);

    // Act
    UserGroupsResponse? actual = await _fixture.SonarCloudClient.UserGroups(_fixture.BearerToken, _fixture.OrganizationKey, _fixture.LoginKey, pageIndex: pageIndex);

    // Assert
    Assert.NotNull(actual);
    AssertPagingInfo(expected, actual);
    Assert.Empty(actual.Groups);

  }

  private static UserGroupsResponse GetExpectedResponse(int pageIndex = 1, int pageSize = 25, int total = 2)
  {
    return new()
    {
      Paging = new()
      {
        PageIndex = pageIndex,
        PageSize = pageSize,
        Total = total
      },
      Groups = new List<UserGroupResponse>()
      {
        new()
        {
          Id = 337936,
          Name = "Members",
          Description = "All members of the organization",
          Selected = true,
          Default = true
        },
        new()
        {
          Id = 337935,
          Name = "Owners",
          Description = "Owners of organization",
          Selected = true,
          Default = false
        }
      }
    };
  }

  private static UserGroupsResponse GetExpectedNoResultsResponse(int pageIndex = 1, int pageSize = 25, int total = 2)
  {
    return new()
    {
      Paging = new()
      {
        PageIndex = pageIndex,
        PageSize = pageSize,
        Total = total
      }
    };
  }

  private static void AssertPagingInfo(UserGroupsResponse expected, UserGroupsResponse? actual)
  {
    if (actual is not null)
    {
      Assert.Equal(expected.Paging.PageIndex, actual.Paging.PageIndex);
      Assert.Equal(expected.Paging.PageSize, actual.Paging.PageSize);
      Assert.Equal(expected.Paging.Total, actual.Paging.Total);
    }
  }

  private static void AssertGroupCount(int expectedGroupCount, UserGroupsResponse? actual)
  {
    if (actual is not null)
    {
      Assert.NotNull(actual.Groups);
      Assert.Equal(expectedGroupCount, actual.Groups.Count);
    }
  }

  private static void AssertGroupDetails(UserGroupsResponse expected, UserGroupsResponse? actual)
  {
    if (actual is not null)
    {
      Assert.Equal(expected.Groups[0].Id, actual.Groups[0].Id);
      Assert.Equal(expected.Groups[0].Name, actual.Groups[0].Name);
      Assert.Equal(expected.Groups[0].Description, actual.Groups[0].Description);
      Assert.Equal(expected.Groups[0].Selected, actual.Groups[0].Selected);
      Assert.Equal(expected.Groups[0].Default, actual.Groups[0].Default);
    }
  }

}