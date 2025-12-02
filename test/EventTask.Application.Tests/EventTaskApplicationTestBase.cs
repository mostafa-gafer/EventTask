using Volo.Abp.Modularity;

namespace EventTask;

public abstract class EventTaskApplicationTestBase<TStartupModule> : EventTaskTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
