using Common.Authorization;
using Infrastructure.Models;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed;

public class Seeder
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _dbContext;

    public Seeder(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    public async Task SeedDataBaseAsync()
    {
        await CheckAndApplyPendingMigrationsAsync();
        await SeedRolesAsync();
        await SeedAdminUserAsync();
    }

    private async Task CheckAndApplyPendingMigrationsAsync()
    {
        if ( (await _dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await _dbContext.Database.MigrateAsync();
        }
    }

    private async Task SeedRolesAsync()
    {
        foreach (var roleName in ApplicationRoles.DefaultRoles)
        {
            if (await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName)
                is not ApplicationRole role)
            {
                role = new ApplicationRole
                {
                    Name = roleName,
                    Description = $"{roleName} Role."
                };
                await _roleManager.CreateAsync(role);
            }

            if (roleName == ApplicationRoles.Admin)
            {
                await AssignPermissionToRoleAsync(role, ApplicationPermissions.AdminPermission);
            }
            else if (roleName == ApplicationRoles.BasicUser)
            {
                await AssignPermissionToRoleAsync(role, ApplicationPermissions.BasicPermission);

            }
        }
    }

    private async Task AssignPermissionToRoleAsync(ApplicationRole role, IReadOnlyList<ApplicationPermission> permissions)
    {
        var currentClaims = await _roleManager.GetClaimsAsync(role);
        foreach (var permission in permissions)
        {
            if (!currentClaims.Any( claim =>  claim.Type == ApplicationClaims.Permission && claim.Value == permission.Name))
            {
                var claim = new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = ApplicationClaims.Permission,
                    ClaimValue = permission.Name,
                    Description = permission.Description,
                    Group = permission.Group
                };
                await _dbContext.RoleClaims.AddAsync(claim);
                await _dbContext.SaveChangesAsync();
            }


        }
    }

    private async Task SeedAdminUserAsync()
    {
        string adminUserName = ApplicationCredentials.DefaultEmail[..ApplicationCredentials.DefaultEmail.IndexOf("@")]
            .ToLowerInvariant();
        var adminUser = new ApplicationUser
        {
            FirstName = "Reza",
            LastName = "Jafarpoor",
            Email = ApplicationCredentials.DefaultEmail,
            UserName = adminUserName,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            NormalizedEmail = ApplicationCredentials.DefaultEmail.ToLowerInvariant(),
            NormalizedUserName = adminUserName.ToUpperInvariant(),
            IsActive = true
        };
        if (! await _userManager.Users.AnyAsync(u => u.Email == ApplicationCredentials.DefaultEmail))
        {
            var password = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = password.HashPassword(adminUser, ApplicationCredentials.DefaultPassword);
            await _userManager.CreateAsync(adminUser);
        }

        if ((! await _userManager.IsInRoleAsync(adminUser, ApplicationRoles.BasicUser)) 
             ||
             (!await _userManager.IsInRoleAsync(adminUser, ApplicationRoles.BasicUser)))
        {
            await _userManager.AddToRoleAsync(adminUser, ApplicationRoles.Admin);
        }
        
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}