using TaleLearnCode.SonarCloudNet.Parameters;

namespace TaleLearnCode.SonarCloudNet;

public partial class SonarCloudClient
{

  private const string _route_Search = "users/search";
  private const string _route_UserGroups = "users/groups";

  private const string _parameterName_Selected = "selected";

  private const int _parameterDefault_UserSearch_PageSize = 50;
  private const string? _parameterDefault_UserSearch_Query = null;

  private const int _parameterDefault_UserGroups_PageSize = 25;

  /// <summary>
  /// Gets a list of active users with the specified search criteria.
  /// </summary>
  /// <param name="pageIndex">The page index of the search results.</param>
  /// <param name="pageSize">The page size of the search results.</param>
  /// <param name="query">The search query.</param>
  /// <returns>A <see cref="UserSearchResponse"/> representing the search results.</returns>
  public async Task<UserSearchResponse?> UserSearch(
    int pageIndex = _parameterDefault_PageIndex,
    int pageSize = _parameterDefault_UserSearch_PageSize,
    string? query = _parameterDefault_UserSearch_Query)
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
  public async Task<UserSearchResponse?> UserSearch(
    string bearerToken,
    int pageIndex = _parameterDefault_PageIndex,
    int pageSize = _parameterDefault_UserSearch_PageSize,
    string? query = _parameterDefault_UserSearch_Query)
  {

    if (string.IsNullOrWhiteSpace(bearerToken)) throw new ArgumentNullException(nameof(bearerToken));
    if (pageIndex < 1) throw new ArgumentOutOfRangeException(nameof(pageIndex));
    if (pageSize > _parameterMax_PageSize) throw new ArgumentException($"The {nameof(pageSize)} must be less than or equal to {_parameterMax_PageSize}.", nameof(pageSize));
    if (query?.Length < 2) throw new ArgumentException($"The {nameof(query)} must be at least two characters in length.");

    Dictionary<string, string>? queryStringParams = null;
    AddParameterToQueryStringDictionary(ref queryStringParams, _parameterName_PageIndex, pageIndex.ToString(), _parameterDefault_PageIndex.ToString());
    AddParameterToQueryStringDictionary(ref queryStringParams, _parameterName_PageSize, pageSize.ToString(), _parameterDefault_UserSearch_PageSize.ToString());
    AddParameterToQueryStringDictionary(ref queryStringParams, _parameterName_Query, query, _parameterDefault_UserSearch_Query);

    UserSearchResponse? sonarResponse = JsonSerializer.Deserialize<UserSearchResponse>(await GetStringAsync(_route_Search, bearerToken, queryStringParams), _jsonSerializerOptions);

    return sonarResponse;

  }

  /// <summary>
  /// Gets a list of groups a user belongs to.
  /// </summary>
  /// <param name="organizationKey">Key of the organization of the user to be looked up.</param>
  /// <param name="loginKey">Key of the user whose groups are to be returned.</param>
  /// <param name="selected">Depending on the value, show only selected, deselected, or all values.  Default is "selected".</param>
  /// <param name="pageIndex">The page index of the search results. The default is 1.</param>
  /// <param name="pageSize">The page size of the search results. The default is 25.</param>
  /// <returns>A <see cref="UserGroupResponse"/> representing the API results.</returns>
  /// <exception cref="System.ArgumentNullException">Thrown if the <paramref name="bearerToken"/>, <paramref name="organizationKey"/>, or <paramref name="loginKey"/> are not provided.</exception>
  /// <exception cref="System.ArgumentOutOfRangeException">pageIndex</exception>
  /// <exception cref="System.ArgumentException">Thrown if <paramref name="pageSize"/> is greater than 500.</exception>
  /// <exception cref="UnauthorizedException">Thrown if the supplied bearer token is invalid or not authorized to perform the specified query.</exception>
  /// <exception cref="TooManyRequestsException">Thrown when attempting to make too many requests to the SonarCloud Web API within a specified time.</exception>
  /// <exception cref="ForbiddenException">Thrown when the bearer token does not have sufficient privileges.</exception>
  /// <remarks>Requires the permission 'Administer' on the organization.</remarks>
  public async Task<UserGroupsResponse?> UserGroups(
    string organizationKey,
    string loginKey,
    int pageIndex = _parameterDefault_PageIndex,
    int pageSize = _parameterDefault_UserGroups_PageSize,
    string selected = UserGroupsSelected.DefaultValue)
    => await UserGroups(_bearerToken, organizationKey, loginKey, pageIndex, pageSize, selected);


  /// <summary>
  /// Gets a list of groups a user belongs to.
  /// </summary>
  /// <param name="bearerToken">The bearer token to authorize the API request.</param>
  /// <param name="organizationKey">Key of the organization of the user to be looked up.</param>
  /// <param name="loginKey">Key of the user whose groups are to be returned.</param>
  /// <param name="selected">Depending on the value, show only selected, deselected, or all values.  Default is "selected".</param>
  /// <param name="pageIndex">The page index of the search results. The default is 1.</param>
  /// <param name="pageSize">The page size of the search results. The default is 25.</param>
  /// <returns>A <see cref="UserGroupResponse"/> representing the API results.</returns>
  /// <exception cref="System.ArgumentNullException">Thrown if the <paramref name="bearerToken"/>, <paramref name="organizationKey"/>, or <paramref name="loginKey"/> are not provided.</exception>
  /// <exception cref="System.ArgumentOutOfRangeException">pageIndex</exception>
  /// <exception cref="System.ArgumentException">Thrown if <paramref name="pageSize"/> is greater than 500.</exception>
  /// <exception cref="UnauthorizedException">Thrown if the supplied bearer token is invalid or not authorized to perform the specified query.</exception>
  /// <exception cref="TooManyRequestsException">Thrown when attempting to make too many requests to the SonarCloud Web API within a specified time.</exception>
  /// <exception cref="ForbiddenException">Thrown when the bearer token does not have sufficient privileges.</exception>
  /// <remarks>Requires the permission 'Administer' on the organization.</remarks>
  public async Task<UserGroupsResponse?> UserGroups(
    string bearerToken,
    string organizationKey,
    string loginKey,
    int pageIndex = _parameterDefault_PageIndex,
    int pageSize = _parameterDefault_UserGroups_PageSize,
    string selected = UserGroupsSelected.DefaultValue)
  {

    if (string.IsNullOrWhiteSpace(bearerToken)) throw new ArgumentNullException(nameof(bearerToken));
    if (string.IsNullOrWhiteSpace(organizationKey)) throw new ArgumentNullException(nameof(organizationKey));
    if (string.IsNullOrWhiteSpace(loginKey)) throw new ArgumentNullException(nameof(loginKey));
    if (!UserGroupsSelected.PossibleValues.Contains(selected)) throw new ArgumentOutOfRangeException(nameof(selected));
    if (pageIndex < 1) throw new ArgumentOutOfRangeException(nameof(pageIndex));
    if (pageSize > _parameterMax_PageSize) throw new ArgumentException($"The {nameof(pageSize)} must be less then or equal to {_parameterMax_PageSize}", nameof(pageSize));

    Dictionary<string, string>? queryStringParams = null;
    AddParameterToQueryStringDictionary(ref queryStringParams, _parameterName_Organization, organizationKey, string.Empty);
    AddParameterToQueryStringDictionary(ref queryStringParams, _parameterName_Login, loginKey, string.Empty);
    AddParameterToQueryStringDictionary(ref queryStringParams, _parameterName_Selected, selected, UserGroupsSelected.DefaultValue);
    AddParameterToQueryStringDictionary(ref queryStringParams, _parameterName_PageIndex, pageIndex.ToString(), _parameterDefault_PageIndex.ToString());
    AddParameterToQueryStringDictionary(ref queryStringParams, _parameterName_PageSize, pageSize.ToString(), _parameterDefault_UserSearch_PageSize.ToString());

    UserGroupsResponse? sonarResponse = JsonSerializer.Deserialize<UserGroupsResponse>(await GetStringAsync(_route_UserGroups, bearerToken, queryStringParams), _jsonSerializerOptions);

    return sonarResponse;

  }

}