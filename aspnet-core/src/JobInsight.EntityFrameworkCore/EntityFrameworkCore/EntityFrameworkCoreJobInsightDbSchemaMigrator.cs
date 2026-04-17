using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JobInsight.Data;
using Volo.Abp.DependencyInjection;

namespace JobInsight.EntityFrameworkCore;

public class EntityFrameworkCoreJobInsightDbSchemaMigrator
    : IJobInsightDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreJobInsightDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the JobInsightDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<JobInsightDbContext>()
            .Database
            .MigrateAsync();
    }
}
