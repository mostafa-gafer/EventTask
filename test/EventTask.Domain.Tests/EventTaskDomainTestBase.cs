using Volo.Abp.Modularity;

namespace EventTask;

/* Inherit from this class for your domain layer tests. */
public abstract class EventTaskDomainTestBase<TStartupModule> : EventTaskTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
