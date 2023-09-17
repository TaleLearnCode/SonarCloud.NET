using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using TaleLearnCode.SonarCloudNet.Exceptions;

namespace TaleLearnCode.SonarCloudNet;

/// <summary>
/// Client for accessing the SonarCloud API.
/// Implements the <see cref="IDisposable" />
/// </summary>
/// <seealso cref="IDisposable" />
public partial class SonarCloudClient : IDisposable
{

  private readonly HttpClient _httpClient;
  private readonly string _bearerToken = string.Empty;

  private const string _baseApiAddress = "https://sonarcloud.io/api/";

  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
  };

  /// <summary>
  /// Initializes a new instance of the <see cref="SonarCloudClient"/> class.
  /// </summary>
  public SonarCloudClient()
  {
    _httpClient = new HttpClient();
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="SonarCloudClient"/> class.
  /// </summary>
  /// <param name="httpClient">An instance of <see cref="HttpClient"/> to be used for accessing the SonarCloud Web API.</param>
  public SonarCloudClient(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="SonarCloudClient"/> class.
  /// </summary>
  /// <param name="bearerToken">The bearer token to be used for authentication to the SonarCloud Web API.</param>
  public SonarCloudClient(string bearerToken)
  {
    _bearerToken = bearerToken;
    _httpClient = new HttpClient();
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="SonarCloudClient"/> class.
  /// </summary>
  /// <param name="httpClient">An instance of <see cref="HttpClient"/> to be used for accessing the SonarCloud Web API.</param>
  /// <param name="bearerToken">The bearer token to be used for authentication to the SonarCloud Web API.</param>
  public SonarCloudClient(HttpClient httpClient, string bearerToken)
  {
    _httpClient = httpClient;
    _bearerToken = bearerToken;
  }

  /// <summary>
  /// Gets the base SonarCloud Web API address.
  /// </summary>
  /// <value>The base SonarCloud Web API address.</value>
  public string BaseApiAddress => _baseApiAddress;

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
  /// <returns><c>true</c> if disposing was successful, <c>false</c> otherwise.</returns>
  protected virtual bool Dispose(bool disposing)
  {
    if (disposing)
      _httpClient.Dispose();
    return true;
  }

  /// <summary>
  /// Sends a GET request to the specified SonarCloud Web API endpoint route.
  /// </summary>
  /// <param name="endpointRoute">The endpoint route.</param>
  /// <param name="bearerToken">The bearer token to be used for authentication.</param>
  /// <returns>A string representing the response from the SonarCloud Web API.</returns>
  private async Task<string> GetStringAsync(string endpointRoute, string bearerToken, Dictionary<string, string>? queryStringParams = null)
  {
    InitializeHttpClient(bearerToken);
    try
    {
      string queryString = string.Empty;
      if (queryStringParams is not null)
        queryString = $"?{string.Join("&", queryStringParams.Select(kvp => $"{kvp.Key}={kvp.Value}"))}";
      return await _httpClient.GetStringAsync($"{_baseApiAddress}{endpointRoute}{queryString}");
    }
    catch (HttpRequestException ex) when (ex.Message.StartsWith("Response status code does not indicate success: 401"))
    {
      throw new UnauthorizedException(ex);
    }
    catch (HttpRequestException ex) when (ex.Message.StartsWith("Response status code does not indicate success: 429"))
    {
      throw new TooManyRequestsException(ex);
    }
  }

  /// <summary>
  /// Initializes the HTTP client for calling SonarCloud Web API endpoints.
  /// </summary>
  /// <param name="bearerToken">The bearer token to be used for authentication.</param>
  private void InitializeHttpClient(string bearerToken)
  {
    _httpClient.DefaultRequestHeaders.Accept.Clear();
    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
  }

}