using Xunit;

namespace JobInsight.EntityFrameworkCore;

[CollectionDefinition(JobInsightTestConsts.CollectionDefinitionName)]
public class JobInsightEntityFrameworkCoreCollection : ICollectionFixture<JobInsightEntityFrameworkCoreFixture>
{

}
