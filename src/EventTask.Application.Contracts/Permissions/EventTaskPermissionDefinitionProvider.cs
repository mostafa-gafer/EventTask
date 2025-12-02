using EventTask.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace EventTask.Permissions;

public class EventTaskPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EventTaskPermissions.GroupName);

        var booksPermission = myGroup.AddPermission(EventTaskPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(EventTaskPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(EventTaskPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(EventTaskPermissions.Books.Delete, L("Permission:Books.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EventTaskPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EventTaskResource>(name);
    }
}
