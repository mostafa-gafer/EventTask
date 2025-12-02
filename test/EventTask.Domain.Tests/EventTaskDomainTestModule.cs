using Volo.Abp.Modularity;

namespace EventTask;

[DependsOn(
    typeof(EventTaskDomainModule),
    typeof(EventTaskTestBaseModule)
)]
public class EventTaskDomainTestModule : AbpModule
{

}
