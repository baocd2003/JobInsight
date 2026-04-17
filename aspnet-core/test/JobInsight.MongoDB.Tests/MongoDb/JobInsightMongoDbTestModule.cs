using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace JobInsight.MongoDB;

[DependsOn(
    typeof(JobInsightApplicationTestModule),
    typeof(JobInsightMongoDbModule)
)]
public class JobInsightMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = JobInsightMongoDbFixture.GetRandomConnectionString();
        });
    }
}
