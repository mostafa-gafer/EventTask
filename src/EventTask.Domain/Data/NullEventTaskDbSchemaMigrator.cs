using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EventTask.Data;

/* This is used if database provider does't define
 * IEventTaskDbSchemaMigrator implementation.
 */
public class NullEventTaskDbSchemaMigrator : IEventTaskDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
