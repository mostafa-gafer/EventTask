namespace EventTask.Permissions;

public static class EventTaskPermissions
{
    public const string GroupName = "EventTask";


    public static class Events
    {
        public const string Default = GroupName + ".Events";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Register = Default + ".Register";
        public const string Cancel = Default + ".Cancel";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
