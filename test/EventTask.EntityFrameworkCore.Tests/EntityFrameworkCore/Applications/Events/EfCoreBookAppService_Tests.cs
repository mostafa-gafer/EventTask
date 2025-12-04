using EventTask.Events;
using Xunit;

namespace EventTask.EntityFrameworkCore.Applications.Books;

[Collection(EventTaskTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : EventAppService_Tests<EventTaskEntityFrameworkCoreTestModule>
{

}