namespace Common.Authorization;

public record ApplicationPermission(string Feature, string Action, string Group, string Description, bool IsBasic = false)
{
    public string Name { get; set; }

    public static string PermissionBuilder(string feature, string action)
        => $"Permission.{feature}.{action}";
}