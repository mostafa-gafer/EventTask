using Microsoft.Extensions.Localization;
using EventTask.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EventTask;

[Dependency(ReplaceServices = true)]
public class EventTaskBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<EventTaskResource> _localizer;

    public EventTaskBrandingProvider(IStringLocalizer<EventTaskResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
