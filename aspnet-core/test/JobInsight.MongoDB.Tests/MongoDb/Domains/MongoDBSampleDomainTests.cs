using JobInsight.Samples;
using Xunit;

namespace JobInsight.MongoDB.Domains;

[Collection(JobInsightTestConsts.CollectionDefinitionName)]
public class MongoDBSampleDomainTests : SampleDomainTests<JobInsightMongoDbTestModule>
{

}
