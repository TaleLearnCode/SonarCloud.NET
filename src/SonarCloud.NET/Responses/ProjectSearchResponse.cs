#nullable disable

namespace TaleLearnCode.SonarCloudNet.Responses;

/// <summary>
/// Represents a response containing an organization's projects.
/// </summary>
public class ProjectSearchResponse
{

  /// <summary>
  /// Gets or sets the paging information for the response.
  /// </summary>
  /// <value>The paging.</value>
  public PagingResponse Paging { get; set; }

  /// <summary>
  /// Gets or sets the projects owned by the organization.
  /// </summary>
  /// <value>The projects owned by the organization.</value>
  public List<ProjectResponse> Projects { get; set; }

}