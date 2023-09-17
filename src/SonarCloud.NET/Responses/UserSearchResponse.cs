#nullable disable

namespace TaleLearnCode.SonarCloudNet.Responses;

/// <summary>
/// Represents a response containing user search results.
/// </summary>
public class UserSearchResponse
{

  /// <summary>
  /// Gets or sets the paging information for the response.
  /// </summary>
  /// <value>The paging.</value>
  public PagingResponse Paging { get; set; }

  /// <summary>
  /// Gets or sets the list of users returned in the response.
  /// </summary>
  /// <value>The users.</value>
  public List<UserResponse> Users { get; set; }

}