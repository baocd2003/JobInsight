using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace JobInsight.Data;

/* This is used if database provider does't define
 * IJobInsightDbSchemaMigrator implementation.
 */
public class NullJobInsightDbSchemaMigrator : IJobInsightDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
