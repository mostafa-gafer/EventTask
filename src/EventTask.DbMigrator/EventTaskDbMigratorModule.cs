using EventTask.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EventTask.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EventTaskEntityFrameworkCoreModule),
    typeof(EventTaskApplicationContractsModule)
)]
public class EventTaskDbMigratorModule : AbpModule
{
}
