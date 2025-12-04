using System;
using Volo.Abp.Identity;

namespace EventTask;

public static class EventTaskConsts
{
    public const string DbTablePrefix = "";
    public const string? DbSchema = null;
    public const string AdminEmailDefaultValue = IdentityDataSeedContributor.AdminEmailDefaultValue;
    public const string AdminPasswordDefaultValue = IdentityDataSeedContributor.AdminPasswordDefaultValue;

    // Roles
    public const string AdminRole = "admin";
    public const string AdminNormalizedRole = "ADMIN";
    public const string UserRole = "user";
    public const string UserNormalizedRole = "USER";
}
