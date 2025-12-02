using System.Threading.Tasks;

namespace EventTask.Data;

public interface IEventTaskDbSchemaMigrator
{
    Task MigrateAsync();
}
