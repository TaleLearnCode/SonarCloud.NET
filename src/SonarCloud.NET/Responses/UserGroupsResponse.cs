#nullable disable

namespace TaleLearnCode.SonarCloudNet.Responses;

/// <summary>
/// Represents a response containing a user's groups.
/// </summary>
public class UserGroupsResponse
{

  /// <summary>
  /// Gets or sets the paging information for the response.
  /// </summary>
  /// <value>The paging.</value>
  public PagingResponse Paging { get; set; }

  /// <summary>
  /// Gets the list of groups the specified user is a member of.
  /// </summary>
  /// <value>The groups the specified user is a member of.</value>
  public List<UserGroupResponse> Groups { get; set; }

}
