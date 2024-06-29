using System.Collections.ObjectModel;

namespace Common.Authorization;

public class ApplicationPermissions
{
    private static readonly ApplicationPermission[] Permissions = new ApplicationPermission[]
    {
        new ApplicationPermission(ApplicationFeatures.Users, ApplicationActions.Create, ApplicationRoleGroup.SystemAccess, "Create Users"),
        new ApplicationPermission(ApplicationFeatures.Users, ApplicationActions.Read, ApplicationRoleGroup.SystemAccess, "Read Users"),
        new ApplicationPermission(ApplicationFeatures.Users, ApplicationActions.Update, ApplicationRoleGroup.SystemAccess, "Update Users"),
        new ApplicationPermission(ApplicationFeatures.Users, ApplicationActions.Delete, ApplicationRoleGroup.SystemAccess, "Delete Users"),
        
        new ApplicationPermission(ApplicationFeatures.UserRoles, ApplicationActions.Read, ApplicationRoleGroup.SystemAccess, "Read User Roles"),
        new ApplicationPermission(ApplicationFeatures.UserRoles, ApplicationActions.Update, ApplicationRoleGroup.SystemAccess, "Update User Roles"),
        
        new ApplicationPermission(ApplicationFeatures.Roles, ApplicationActions.Create, ApplicationRoleGroup.SystemAccess, "Create Roles"),
        new ApplicationPermission(ApplicationFeatures.Roles, ApplicationActions.Read, ApplicationRoleGroup.SystemAccess, "Read Roles"),
        new ApplicationPermission(ApplicationFeatures.Roles, ApplicationActions.Update, ApplicationRoleGroup.SystemAccess, "Update Roles"),
        new ApplicationPermission(ApplicationFeatures.Roles, ApplicationActions.Delete, ApplicationRoleGroup.SystemAccess, "Delete Roles"),
        
        new ApplicationPermission(ApplicationFeatures.RoleClaims, ApplicationActions.Read, ApplicationRoleGroup.SystemAccess, "Read RoleClaims"),
        new ApplicationPermission(ApplicationFeatures.RoleClaims, ApplicationActions.Update, ApplicationRoleGroup.SystemAccess, "Update RoleClaims"),
        
        new ApplicationPermission(ApplicationFeatures.Employees, ApplicationActions.Create, ApplicationRoleGroup.SystemAccess, "Create Employees"),
        new ApplicationPermission(ApplicationFeatures.Employees, ApplicationActions.Read, ApplicationRoleGroup.SystemAccess, "Read Employees", IsBasic:true),
        new ApplicationPermission(ApplicationFeatures.Employees, ApplicationActions.Update, ApplicationRoleGroup.SystemAccess, "Update Employees"),
        new ApplicationPermission(ApplicationFeatures.Employees, ApplicationActions.Delete, ApplicationRoleGroup.SystemAccess, "Delete Employees"),
    };
    
    public static IReadOnlyList<ApplicationPermission> AdminPermission { get; } = 
        new ReadOnlyCollection<ApplicationPermission>(Permissions.Where( p => !p.IsBasic).ToList());

    public static IReadOnlyList<ApplicationPermission> BasicPermission { get; } =
        new ReadOnlyCollection<ApplicationPermission>(Permissions.Where(p => p.IsBasic).ToList());
    
}