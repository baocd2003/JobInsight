using Xunit;

namespace JobInsight.MongoDB;

[CollectionDefinition(JobInsightTestConsts.CollectionDefinitionName)]
public class JobInsightMongoCollection : JobInsightMongoDbCollectionFixtureBase
{

}
