using System.Collections.ObjectModel;

namespace Common.Authorization;

public static class ApplicationRoles
{
    public const string Admin = nameof(Admin);
    public const string BasicUser = nameof(BasicUser);

    public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(new []
    {
        Admin,
        BasicUser
    });

    public static bool IsDefault(string roleName) => DefaultRoles.Any(x => x == roleName);

}