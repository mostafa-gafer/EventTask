using EventTask.Samples;
using Xunit;

namespace EventTask.EntityFrameworkCore.Domains;

[Collection(EventTaskTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<EventTaskEntityFrameworkCoreTestModule>
{

}
