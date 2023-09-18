namespace TaleLearnCode.SonarCloudNet.Extensions;

internal static class DateTimeExtensions
{

  internal static string? ToQueryStringValue(this DateTimeOffset? dateTimeOffset)
    => dateTimeOffset?.ToString("yyyy-MM-dTHH:m:sK") ?? null;

}