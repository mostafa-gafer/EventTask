using Xunit;

namespace EventTask.EntityFrameworkCore;

[CollectionDefinition(EventTaskTestConsts.CollectionDefinitionName)]
public class EventTaskEntityFrameworkCoreCollection : ICollectionFixture<EventTaskEntityFrameworkCoreFixture>
{

}
