using Volo.Abp.Modularity;

namespace EventTask;

[DependsOn(
    typeof(EventTaskApplicationModule),
    typeof(EventTaskDomainTestModule)
)]
public class EventTaskApplicationTestModule : AbpModule
{

}
