using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace EventTask;

public class EventTaskDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{

    private readonly IIdentityRoleRepository _roleRepository;
    private readonly IdentityRoleManager _roleManager;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IPermissionDataSeeder _permissionDataSeeder;

    public EventTaskDataSeederContributor(
        IIdentityRoleRepository roleRepository,
        IdentityRoleManager roleManager,
        IGuidGenerator guidGenerator,
        IPermissionDataSeeder permissionDataSeeder)
    {
        _roleRepository = roleRepository;
        _roleManager = roleManager;
        _guidGenerator = guidGenerator;
        _permissionDataSeeder = permissionDataSeeder;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedRolesAsync();
        await SeedPermissionsAsync();
    }

    private async Task SeedRolesAsync()
    {
        var adminRole = await _roleRepository.FindByNormalizedNameAsync(EventTaskConsts.AdminNormalizedRole);

        if (adminRole == null)
        {
            adminRole = new IdentityRole(
                _guidGenerator.Create(),
                EventTaskConsts.AdminRole
            )
            {
                IsDefault = false,
                IsPublic = false
            };

            await _roleManager.CreateAsync(adminRole);
        }

        var userRole = await _roleRepository.FindByNormalizedNameAsync(EventTaskConsts.UserNormalizedRole);

        if (userRole == null)
        {
            userRole = new IdentityRole(
                _guidGenerator.Create(),
                EventTaskConsts.UserRole
            )
            {
                IsDefault = false,
                IsPublic = false
            };

            await _roleManager.CreateAsync(userRole);
        }
    }

    private async Task SeedPermissionsAsync()
    {
        // Grant all event permissions to admin role
        await _permissionDataSeeder.SeedAsync(
            RolePermissionValueProvider.ProviderName,
            EventTaskConsts.UserRole,
            new[]
            {
                    "EventTask.Events",
                    "EventTask.Events.Register",
                    "EventTask.Events.Cancel"
            }
        );
    }
}