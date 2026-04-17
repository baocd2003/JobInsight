using JobInsight.Samples;
using Xunit;

namespace JobInsight.EntityFrameworkCore.Domains;

[Collection(JobInsightTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<JobInsightEntityFrameworkCoreTestModule>
{

}
