using EventTask.Books;
using Xunit;

namespace EventTask.EntityFrameworkCore.Applications.Books;

[Collection(EventTaskTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<EventTaskEntityFrameworkCoreTestModule>
{

}