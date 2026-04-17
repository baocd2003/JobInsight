using System.Threading.Tasks;

namespace JobInsight.Data;

public interface IJobInsightDbSchemaMigrator
{
    Task MigrateAsync();
}
