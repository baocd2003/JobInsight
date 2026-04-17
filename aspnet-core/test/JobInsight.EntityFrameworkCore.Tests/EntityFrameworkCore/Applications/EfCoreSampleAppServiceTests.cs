using JobInsight.Samples;
using Xunit;

namespace JobInsight.EntityFrameworkCore.Applications;

[Collection(JobInsightTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<JobInsightEntityFrameworkCoreTestModule>
{

}
