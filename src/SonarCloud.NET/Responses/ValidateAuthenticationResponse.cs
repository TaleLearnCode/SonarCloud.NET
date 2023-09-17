namespace TaleLearnCode.SonarCloudNet.Responses;

/// <summary>
/// Represents a response for validating authentication.
/// </summary>
internal class ValidateAuthenticationResponse
{

  /// <summary>
  /// Gets or sets a value indicating whether the authentication is valid.
  /// </summary>
  /// <value><c>true</c> if the authentication is valid; otherwise, <c>false</c>.</value>
  public bool Valid { get; set; }

}