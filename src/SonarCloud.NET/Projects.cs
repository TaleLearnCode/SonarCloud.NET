namespace TaleLearnCode.SonarCloudNet;

public partial class SonarCloudClient
{

  public async Task<ProjectSearchResponse?> ProjectSearch(
    string organizationKey,
    DateTimeOffset? analyzedBefore = null,
    bool onProvisionedOnly = ParameterDefaults.OnProvisionedOnly,
    string? query = ParameterDefaults.Query,
    int pageIndex = ParameterDefaults.PageIndex,
    int pageSize = ParameterDefaults.ProjectSearchPageSize)
    => await ProjectSearch(_bearerToken, organizationKey, analyzedBefore, onProvisionedOnly, query, pageIndex, pageSize);

  public async Task<ProjectSearchResponse?> ProjectSearch(
    string bearerToken,
    string organizationKey,
    DateTimeOffset? analyzedBefore = null,
    bool onProvisionedOnly = ParameterDefaults.OnProvisionedOnly,
    string? query = ParameterDefaults.Query,
    int pageIndex = ParameterDefaults.PageIndex,
    int pageSize = ParameterDefaults.ProjectSearchPageSize)
  {

    if (string.IsNullOrWhiteSpace(bearerToken)) throw new ArgumentNullException(nameof(bearerToken));
    if (string.IsNullOrWhiteSpace(organizationKey)) throw new ArgumentNullException(nameof(organizationKey));
    if (query?.Length < 2) throw new ArgumentException($"The {nameof(query)} must be at least two characters in length.");
    if (pageIndex < 1) throw new ArgumentOutOfRangeException(nameof(pageIndex));
    if (pageSize > ParameterMaximums.PageSize) throw new ArgumentException($"The {nameof(pageSize)} must be less then or equal to {ParameterMaximums.PageSize}", nameof(pageSize));

    Dictionary<string, string>? queryStringParams = null;
    AddParameterToQueryStringDictionary(ref queryStringParams, ParameterNames.Organization, organizationKey, null);
    AddParameterToQueryStringDictionary(ref queryStringParams, ParameterNames.AnalyzedBefore, analyzedBefore.ToQueryStringValue(), null);
    AddParameterToQueryStringDictionary(ref queryStringParams, ParameterNames.OnProvisionedOnly, onProvisionedOnly.ToString(), ParameterDefaults.OnProvisionedOnly.ToString());
    AddParameterToQueryStringDictionary(ref queryStringParams, _parameterName_Query, query, _parameterDefault_UserSearch_Query);
    AddParameterToQueryStringDictionary(ref queryStringParams, _parameterName_PageIndex, pageIndex.ToString(), _parameterDefault_PageIndex.ToString());
    AddParameterToQueryStringDictionary(ref queryStringParams, _parameterName_PageSize, pageSize.ToString(), _parameterDefault_UserSearch_PageSize.ToString());

    ProjectSearchResponse? sonarResponse = JsonSerializer.Deserialize<ProjectSearchResponse>(await GetStringAsync(Routes.ProjectSearch, bearerToken, queryStringParams), _jsonSerializerOptions);

    return sonarResponse;

  }

}