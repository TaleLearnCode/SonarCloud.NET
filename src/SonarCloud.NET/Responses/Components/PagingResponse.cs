namespace TaleLearnCode.SonarCloudNet.Responses.Components;

/// <summary>
/// Represents the paging information of a SonarCloud API response.
/// </summary>
public class PagingResponse
{

  /// <summary>
  /// Gets or sets the index of the page returned.
  /// </summary>
  /// <value>The index of the page returned.</value>
  public int PageIndex { get; set; }

  /// <summary>
  /// Gets or sets the size of the page returned.
  /// </summary>
  /// <value>The size of the page returned.</value>
  public int PageSize { get; set; }

  /// <summary>
  /// Gets or sets the total number of items in the query.
  /// </summary>
  /// <value>The total number of items in the query..</value>
  public int Total { get; set; }

}