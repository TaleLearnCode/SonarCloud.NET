#nullable disable

namespace TaleLearnCode.SonarCloudNet.Responses.Components;

/// <summary>
/// Represents a response containing user information.
/// </summary>
public class UserResponse
{

  /// <summary>
  /// Gets or sets the login for the user.
  /// </summary>
  /// <value>The login for the user.</value>
  public string Login { get; set; }

  /// <summary>
  /// Gets or sets the name of the user.
  /// </summary>
  /// <value>The name of the user.</value>
  public string Name { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the user is active.
  /// </summary>
  /// <value><c>true</c> if the user is active; otherwise, <c>false</c>.</value>
  public bool Active { get; set; }

  /// <summary>
  /// Gets or sets the email address of the user.
  /// </summary>
  /// <value>The email address of the user.</value>
  /// <remarks>Only available for the logged-in user.</remarks></remarks>
  public string Email { get; set; }

  /// <summary>
  /// Gets or sets the groups that user belongs to.
  /// </summary>
  /// <value>The groups the user belongs to.</value>
  /// <remarks>Only available for the logged-in user.</remarks></remarks>
  public List<string> Groups { get; set; }

  /// <summary>
  /// Gets or sets the number of tokens the user has.
  /// </summary>
  /// <value>The number of tokens the user has.</value>
  /// <remarks>Only available for the logged-in user.</remarks></remarks>
  public int TokensCount { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether user is local.
  /// </summary>
  /// <value><c>true</c> if the user is local; otherwise, <c>false</c>.</value>
  public bool Local { get; set; }


  /// <summary>
  /// Gets or sets the external provider for the user's authentication.
  /// </summary>
  /// <value>The external authentication provider.</value>
  /// <remarks>Only available for the logged-in user.</remarks></remarks>
  public string ExternalProvider { get; set; }

  /// <summary>
  /// Gets or sets the avatar for the user.
  /// </summary>
  /// <value>The avatar for the user.</value>
  public string Avatar { get; set; }

  /// <summary>
  /// Gets or sets the last connection date of the user.
  /// </summary>
  /// <value>The last connection date of the user.</value>
  /// <remarks>
  /// <para>Only available for the logged-in user.</para>
  /// <para>The value is only updated every hour, so it may not be accurate, for instance when a user authenticates many time sin less than one hour.</para>
  /// </remarks>
  public DateTime LastConnectionDate { get; set; }

}