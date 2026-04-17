using Volo.Abp.Modularity;

namespace JobInsight;

[DependsOn(
    typeof(JobInsightApplicationModule),
    typeof(JobInsightDomainTestModule)
)]
public class JobInsightApplicationTestModule : AbpModule
{

}
