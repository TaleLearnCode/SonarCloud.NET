using Microsoft.Extensions.Configuration;

namespace TaleLearnCode.SonarCloudNet.IntegrationsTests;

public class TestFixture : IDisposable
{

  public SonarCloudClient SonarCloudClient { get; }

  public string BearerToken { get; }

  public TestFixture()
  {

    IConfiguration config = new ConfigurationBuilder()
      .AddJsonFile("AppSettings.json")
      .AddUserSecrets<TestFixture>() // Add this line to read user secrets
      .Build();

    SonarCloudClient = new();

    BearerToken = config["BearerToken"];

  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual bool Dispose(bool disposing)
  {
    if (disposing)
      SonarCloudClient.Dispose();
    return true;
  }

}