using Volo.Abp.Settings;

namespace EventTask.Settings;

public class EventTaskSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(EventTaskSettings.MySetting1));
    }
}
