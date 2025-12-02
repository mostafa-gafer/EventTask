using EventTask.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EventTask.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EventTaskController : AbpControllerBase
{
    protected EventTaskController()
    {
        LocalizationResource = typeof(EventTaskResource);
    }
}
