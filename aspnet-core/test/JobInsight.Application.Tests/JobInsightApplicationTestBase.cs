using Volo.Abp.Modularity;

namespace JobInsight;

public abstract class JobInsightApplicationTestBase<TStartupModule> : JobInsightTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
