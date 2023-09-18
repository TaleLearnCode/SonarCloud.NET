namespace TaleLearnCode.SonarCloudNet.Parameters;

internal static class UserGroupsSelected
{

  private const string _possibleValue_All = "all";
  private const string _possibleValue_Deselected = "deselected";
  private const string _possibleValue_Selected = "selected";

  internal const string DefaultValue = "selected";

  internal static List<string> PossibleValues { get; } = new() { _possibleValue_All, _possibleValue_Deselected, _possibleValue_Selected };

  internal static bool IsParameterValueValid(string parameterValue)
  {
    return PossibleValues.Contains(parameterValue);
  }

}