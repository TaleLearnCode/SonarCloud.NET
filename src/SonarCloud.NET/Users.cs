namespace TaleLearnCode.SonarCloudNet;

public partial class SonarCloudClient
{

  private const string _route_Search = "users/search";

  private const string _parameter_PageIndex = "p";
  private const string _parameter_PageSize = "ps";
  private const string _parameter_Query = "q";

  private const int _defaultParameter_PageIndex = 1;
  private const int _defaultParameter_PageSize = 50;
  private const string? _defaultParameter_Query = null;

  /// <summary>
  /// Gets a list of active users with the specified search criteria.
  /// </summary>
  /// <param name="pageIndex">The page index of the search results.</param>
  /// <param name="pageSize">The page size of the search results.</param>
  /// <param name="query">The search query.</param>
  /// <returns>A <see cref="UserSearchResponse"/> representing the search results.</returns>
  public async Task<UserSearchResponse?> UserSearch(int pageIndex = 1, int pageSize = 50, string? query = null)
    => await UserSearch(_bearerToken, pageIndex, pageSize, query);

  /// <summary>
  /// Gets a list of active users with the specified search criteria.
  /// </summary>
  /// <param name="bearerToken">The bearer token to authorize the API request.</param>
  /// <param name="pageIndex">The page index of the search results.</param>
  /// <param name="pageSize">The page size of the search results.</param>
  /// <param name="query">The search query.</param>
  /// <returns>A <see cref="UserSearchResponse"/> representing the search results.</returns>
  /// <exception cref="ArgumentNullException">Thrown if the bearerToken is not provided.</exception>
  /// <exception cref="ArgumentException">Thrown if the <paramref name="pageSize"/> is greater than 500 or the <paramref name="query"/> is less than 2 characters.</exception>
  /// <exception cref="UnauthorizedException">Thrown if the supplied bearer token is invalid or not authorized to perform the specified query.</exception>
  /// <exception cref="TooManyRequestsException">Thrown when attempting to make too many requests to the SonarCloud Web API within a specified time.</exception>
  public async Task<UserSearchResponse?> UserSearch(string bearerToken, int pageIndex = 1, int pageSize = 50, string? query = _defaultParameter_Query)
  {

    if (string.IsNullOrWhiteSpace(bearerToken)) throw new ArgumentNullException(nameof(bearerToken));
    if (pageSize > 500) throw new ArgumentException($"The {nameof(pageSize)} must be less than or equal to 500.");
    if (query?.Length < 2) throw new ArgumentException($"The {nameof(query)} must be at least two characters in length.");

    Dictionary<string, string>? queryStringParams = null;
    AddParameterToQueryString(ref queryStringParams, _parameter_PageIndex, pageIndex.ToString(), _defaultParameter_PageIndex.ToString());
    AddParameterToQueryString(ref queryStringParams, _parameter_PageSize, pageSize.ToString(), _defaultParameter_PageSize.ToString());
    AddParameterToQueryString(ref queryStringParams, _parameter_Query, query, _defaultParameter_Query);

    UserSearchResponse? sonarResponse = JsonSerializer.Deserialize<UserSearchResponse>(await GetStringAsync(_route_Search, bearerToken, queryStringParams), _jsonSerializerOptions);

    return sonarResponse;

  }

  private static void AddParameterToQueryString(
    ref Dictionary<string, string>? queryStringParams,
    string parameterKey,
    string? parameterValue,
    string? defaultValue)
  {
    if (parameterValue is not null && parameterValue != defaultValue)
    {
      queryStringParams ??= new();
      queryStringParams.Add(parameterKey, parameterValue);
    }
  }
}