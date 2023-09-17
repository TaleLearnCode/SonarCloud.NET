using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaleLearnCode.SonarCloudNet.UnitTests;

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
  /// Gets the HTTP message handler to be used by the tests.
  /// </summary>
  /// <value>The HTTP message handler to be used by the tests.</value>
  public Mock<HttpMessageHandler> MockedHttpMessageHandler { get; }

  //
  public readonly JsonSerializerOptions JsonSerializerOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
  };


  /// <summary>
  /// Gets the bearer token to be used by the tests.
  /// </summary>
  /// <value>The bearer token to be used by the tests.</value>
  public string BearerToken { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="TestFixture"/> class.
  /// </summary>
  public TestFixture()
  {
    MockedHttpMessageHandler = new Mock<HttpMessageHandler>();
    BearerToken = Guid.NewGuid().ToString();
    SonarCloudClient = new(new HttpClient(MockedHttpMessageHandler.Object), BearerToken);
  }

  public HttpResponseMessage GenerateHttpResponseMessage<T>(T contentObject, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    => new()
    {
      StatusCode = httpStatusCode,
      Content = new StringContent(JsonSerializer.Serialize<T>(contentObject, JsonSerializerOptions), new MediaTypeWithQualityHeaderValue("application/json"))
    };

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
  protected virtual bool Dispose(bool disposing) => true;

}