using Microsoft.Extensions.Configuration;

namespace TaleLearnCode.SonarCloudNet.IntegrationsTests;

/// <summary>
/// Represents the test fixture for the unit tests.
/// Implements the <see cref="IDisposable" />
/// </summary>
/// <seealso cref="IDisposable" />
/// <remarks>Contains data and/or setup code that is used by the assembly's tests.</remarks>
public class TestFixture : IDisposable
{

  /// <summary>
  /// Gets the <see cref="SonarCloudClient"/> instance to be used by the tests.
  /// </summary>
  /// <value>The sonar cloud client to be used by the tests.</value>
  public SonarCloudClient SonarCloudClient { get; }

  /// <summary>
  /// Gets the bearer token to be used by the tests.
  /// </summary>
  /// <value>The bearer token to be used by the tests.</value>
  public string BearerToken { get; }

  /// <summary>
  /// Gets the organization key to be used by the tests.
  /// </summary>
  /// <value>The organization key to be used by the tests.</value>
  public string OrganizationKey { get; }

  /// <summary>
  /// Gets the login key to be used by the tests.
  /// </summary>
  /// <value>The login key to be used by the tests.</value>
  public string LoginKey { get; }

  /// <summary>
  /// Gets the secondary organization key to be used by the tests.
  /// </summary>
  /// <value>The secondary organization key to be used by the tests.</value>
  public string SecondaryOrganizationKey { get; }

  /// <summary>
  /// Gets the secondary login key to be used by the tests.
  /// </summary>
  /// <value>The secondary login key to be used by the tests.</value>
  public string SecondaryLoginKey { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="TestFixture"/> class.
  /// </summary>
  public TestFixture()
  {

    IConfiguration config = new ConfigurationBuilder()
      .AddJsonFile("AppSettings.json")
      .AddUserSecrets<TestFixture>() // Add this line to read user secrets
      .Build();

    SonarCloudClient = new();

    BearerToken = config["BearerToken"];
    OrganizationKey = config["OrganizationKey"];
    LoginKey = config["LoginKey"];
    SecondaryOrganizationKey = config["SecondaryOrganizationKey"];
    SecondaryLoginKey = config["SecondaryLoginKey"];

  }

  /// <summary>
  /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
  /// </summary>
  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  /// <summary>
  /// Releases unmanaged and - optionally - managed resources.
  /// </summary>
  /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
  /// <returns><c>true</c> if the dispose is successful, <c>false</c> otherwise.</returns>
  protected virtual bool Dispose(bool disposing)
  {
    if (disposing)
      SonarCloudClient.Dispose();
    return true;
  }

}