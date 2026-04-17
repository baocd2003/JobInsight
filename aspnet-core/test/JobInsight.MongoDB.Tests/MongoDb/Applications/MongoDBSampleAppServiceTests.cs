using JobInsight.MongoDB;
using JobInsight.Samples;
using Xunit;

namespace JobInsight.MongoDb.Applications;

[Collection(JobInsightTestConsts.CollectionDefinitionName)]
public class MongoDBSampleAppServiceTests : SampleAppServiceTests<JobInsightMongoDbTestModule>
{

}
