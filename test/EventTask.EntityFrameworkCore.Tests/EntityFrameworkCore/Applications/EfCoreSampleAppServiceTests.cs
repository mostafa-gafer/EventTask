using EventTask.Samples;
using Xunit;

namespace EventTask.EntityFrameworkCore.Applications;

[Collection(EventTaskTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<EventTaskEntityFrameworkCoreTestModule>
{

}
