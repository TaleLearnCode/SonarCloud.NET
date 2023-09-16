namespace TaleLearnCode.SonarCloudNet.IntegrationsTests.Authentication;

public class Validate : IClassFixture<TestFixture>
{

  private readonly TestFixture _fixture;

  public Validate(TestFixture fixture)
  {
    _fixture = fixture;
  }

  [Fact]
  public async Task ValidBearerToken_ReturnsTrue()
    => await ActAssertAsync(_fixture.BearerToken, true);

  [Fact]
  public async Task InvalidBearerToken_ReturnsFalse()
    => await ActAssertAsync("InvalidBearerToken", false);

  private async Task ActAssertAsync(string bearerToken, bool expected)
    => Assert.Equal(expected, await _fixture.SonarCloudClient.Validate(bearerToken));

}