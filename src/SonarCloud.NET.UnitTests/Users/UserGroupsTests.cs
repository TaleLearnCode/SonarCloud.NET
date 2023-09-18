#nullable disable

namespace TaleLearnCode.SonarCloudNet.UnitTests.Users;

public class UserGroupsTests : IClassFixture<TestFixture>
{

  private readonly TestFixture _fixture;

  public UserGroupsTests(TestFixture fixture) => _fixture = fixture;

  [Fact]
  public async Task NullBearerToken_ThrowsArgumentNullException() => await ParameterThrowsArgumentNullException(null, "organizationKey", "loginKey");

  [Fact]
  public async Task EmptyBearerToken_ThrowsArgumentNullException() => await ParameterThrowsArgumentNullException(string.Empty, "organizationKey", "loginKey");

  [Fact]
  public async Task WhitespaceBearerToken_ThrowsArgumentNullException() => await ParameterThrowsArgumentNullException(" ", "organizationKey", "loginKey");

  [Fact]
  public async Task NullOrganizationKeyToken_ThrowsArgumentNullException() => await ParameterThrowsArgumentNullException("bearerToken", null, "loginKey");

  [Fact]
  public async Task EmptyOrganizationKeyToken_ThrowsArgumentNullException() => await ParameterThrowsArgumentNullException("bearerToken", string.Empty, "loginKey");

  [Fact]
  public async Task WhitespaceOrganizationKeyToken_ThrowsArgumentNullException() => await ParameterThrowsArgumentNullException("bearerToken", string.Empty, "loginKey");

  [Fact]
  public async Task NullLoginKeyToken_ThrowsArgumentNullException() => await ParameterThrowsArgumentNullException("bearerToken", "organizationKey", null);

  [Fact]
  public async Task EmptyLoginKeyToken_ThrowsArgumentNullException() => await ParameterThrowsArgumentNullException("bearerToken", "organizationKey", string.Empty);

  [Fact]
  public async Task WhitespaceLoginKeyToken_ThrowsArgumentNullException() => await ParameterThrowsArgumentNullException("bearerToken", "organizationKey", " ");

  [Fact]
  public async Task InvalidSelected_ThrowsArgumentOutOfRangeException()
  {

    // Arrange
    string selected = "invalid";

    // Act
    async Task Act() => await _fixture.SonarCloudClient.UserGroups("organizationKey", "loginKey", selected: selected);

    // Assert
    await Assert.ThrowsAsync<ArgumentOutOfRangeException>(Act);
  }

  [Fact]
  public async Task PageIndexLessThan1_ThrowsArgumentOutOfRangeException()
  {

    // Arrange
    int pageIndex = -1;

    // Act
    async Task Act() => await _fixture.SonarCloudClient.UserGroups("organizationKey", "loginKey", pageIndex: pageIndex);

    // Assert
    await Assert.ThrowsAsync<ArgumentOutOfRangeException>(Act);

  }

  [Fact]
  public async Task PageSizeGreaterThen500_ThrowsArgumentException()
  {

    // Arrange
    int pageSize = 501;

    // Act
    async Task Act() => await _fixture.SonarCloudClient.UserGroups("organizationKey", "loginKey", pageSize: pageSize);

    // Assert
    await Assert.ThrowsAsync<ArgumentException>(Act);

  }

  [Fact]
  public async Task ReturnsUserGroupsResponse()
  {

    // Arrange
    UserGroupsResponse expected = new()
    {
      Paging = new()
      {
        PageIndex = 1,
        PageSize = 25,
        Total = 1
      },
      Groups = new List<UserGroupResponse>
      {
        new()
        {
          Id = 242,
          Name = "name",
          Description = "description",
          Selected = true,
          Default = true
        }
      }
    };

    HttpResponseMessage httpResponseMessage = _fixture.GenerateHttpResponseMessage(expected);

    _fixture.MockedHttpMessageHandler.Protected()
      .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
      .ReturnsAsync(httpResponseMessage);

    // Act
    UserGroupsResponse actual = await _fixture.SonarCloudClient.UserGroups("organizationKey", "loginKey");

    // Assert
    Assert.NotNull(actual);
    Assert.Equal(expected.Paging.PageIndex, actual.Paging.PageIndex);
    Assert.Equal(expected.Paging.PageSize, actual.Paging.PageSize);
    Assert.Equal(expected.Paging.Total, actual.Paging.Total);
    Assert.Equal(expected.Groups.Count, actual.Groups.Count);
    Assert.Equal(expected.Groups[0].Id, actual.Groups[0].Id);
    Assert.Equal(expected.Groups[0].Name, actual.Groups[0].Name);
    Assert.Equal(expected.Groups[0].Description, actual.Groups[0].Description);
    Assert.Equal(expected.Groups[0].Selected, actual.Groups[0].Selected);
    Assert.Equal(expected.Groups[0].Default, actual.Groups[0].Default);



  }

  private async Task ParameterThrowsArgumentNullException(string bearerToken, string organizationKey, string loginKey)
  {
    async Task Act() => await _fixture.SonarCloudClient.UserGroups(bearerToken, organizationKey, loginKey);
    await Assert.ThrowsAsync<ArgumentNullException>(Act);
  }

}