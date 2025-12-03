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

        var eventsPermission = myGroup.AddPermission(EventTaskPermissions.Events.Default, L("Permission:Events"));
        eventsPermission.AddChild(EventTaskPermissions.Events.Create, L("Permission:Events.Create"));
        eventsPermission.AddChild(EventTaskPermissions.Events.Edit, L("Permission:Events.Edit"));
        eventsPermission.AddChild(EventTaskPermissions.Events.Delete, L("Permission:Events.Delete"));
        eventsPermission.AddChild(EventTaskPermissions.Events.Register, L("Permission:Events.Register "));
        eventsPermission.AddChild(EventTaskPermissions.Events.Cancel, L("Permission:Events.Cancel"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(EventTaskPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EventTaskResource>(name);
    }
}
