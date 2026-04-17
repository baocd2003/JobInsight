using Volo.Abp.Modularity;

namespace JobInsight;

[DependsOn(
    typeof(JobInsightDomainModule),
    typeof(JobInsightTestBaseModule)
)]
public class JobInsightDomainTestModule : AbpModule
{

}
