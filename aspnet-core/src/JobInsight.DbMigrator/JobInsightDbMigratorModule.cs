using JobInsight.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace JobInsight.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(JobInsightEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(JobInsightApplicationContractsModule)
    )]
public class JobInsightDbMigratorModule : AbpModule
{
}
