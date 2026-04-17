using Volo.Abp.Modularity;

namespace JobInsight;

/* Inherit from this class for your domain layer tests. */
public abstract class JobInsightDomainTestBase<TStartupModule> : JobInsightTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
