#nullable disable

namespace TaleLearnCode.SonarCloudNet.Responses.Components;

/// <summary>
/// Represents a response containing user group information.
/// </summary>
public class UserGroupResponse
{

  /// <summary>
  /// Gets or sets the identifier of the user group
  /// </summary>
  /// <value>The identifier of the user group.</value>
  public int Id { get; set; }

  /// <summary>
  /// Gets or sets the name of the user group.
  /// </summary>
  /// <value>The name of the user group.</value>
  public string Name { get; set; }

  /// <summary>
  /// Gets or sets the description for the user group.
  /// </summary>
  /// <value>The description for the user group.</value>
  public string Description { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the user group is selected.
  /// </summary>
  /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
  public bool Selected { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the user group is the default user group for user in the organization.
  /// </summary>
  /// <value><c>true</c> if default; otherwise, <c>false</c>.</value>
  public bool Default { get; set; }

}