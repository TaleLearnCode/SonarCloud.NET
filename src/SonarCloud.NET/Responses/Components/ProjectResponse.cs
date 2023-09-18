#nullable disable

namespace TaleLearnCode.SonarCloudNet.Responses.Components;

/// <summary>
/// Represents a response containing project information.
/// </summary>
public class ProjectResponse
{

  /// <summary>
  /// Gets or sets the key for the organization owning the project.
  /// </summary>
  /// <value>The organization owning the project.</value>
  public string Organization { get; set; }

  /// <summary>
  /// Gets or sets the key for the project.
  /// </summary>
  /// <value>The key for the project.</value>
  public string Key { get; set; }

  /// <summary>
  /// Gets or sets the name of the project.
  /// </summary>
  /// <value>The name of the project..</value>
  public string Name { get; set; }

  /// <summary>
  /// Gets or sets the qualifier for the project.
  /// </summary>
  /// <value>The qualifier for the project.</value>
  public string Qualifier { get; set; }

  /// <summary>
  /// Gets or sets the visibility of the project.
  /// </summary>
  /// <value>The visibility of the project.</value>
  public string Visibility { get; set; }

  /// <summary>
  /// Gets or sets the last analysis date of the project.
  /// </summary>
  /// <value>The last analysis date of the project.</value>
  public DateTime LastAnalysisDate { get; set; }

  /// <summary>
  /// Gets or sets the revision for the project.
  /// </summary>
  /// <value>The revision for the project.</value>
  public string Revision { get; set; }

}