using EventTask.Localization;
using Volo.Abp.Application.Services;

namespace EventTask;

/* Inherit your application services from this class.
 */
public abstract class EventTaskAppService : ApplicationService
{
    protected EventTaskAppService()
    {
        LocalizationResource = typeof(EventTaskResource);
    }
}
