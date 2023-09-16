namespace TaleLearnCode.SonarCloudNet;

public partial class SonarCloudClient
{

  private const string _route_Validate = "authentication/validate";

  /// <summary>
  /// Check credentials of the default user (as identified by the initialized bearer token).
  /// </summary>
  /// <returns><c>true</c> if authenticated, <c>false</c> otherwise.</returns>
  public async Task<bool> Validate() => await Validate(_bearerToken);

  /// <summary>
  /// Check credentials of the specified user (as identified by the <paramref name="bearerToken"/>).
  /// </summary>
  /// <param name="bearerToken">The bearer token to be used for authentication.</param>
  /// <returns><c>true</c> if authenticated, <c>false</c> otherwise.</returns>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="bearerToken"/> is null or empty.</exception>
  public async Task<bool> Validate(string bearerToken)
  {
    if (string.IsNullOrWhiteSpace(bearerToken)) throw new ArgumentNullException(nameof(bearerToken));
    ValidateAuthenticationSonarResponse? sonarResponse
      = JsonSerializer.Deserialize<ValidateAuthenticationSonarResponse>(await GetAsync(_route_Validate, bearerToken), _jsonSerializerOptions);
    return sonarResponse?.Valid ?? false;
  }

}