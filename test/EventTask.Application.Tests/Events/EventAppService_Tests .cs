using EventTask.Events.Dtos;
using EventTask.Events.Interfaces;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;

namespace EventTask.Events;

public abstract class EventAppService_Tests<TStartupModule> : EventTaskApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IEventAppService _eventAppService;

    protected EventAppService_Tests()
    {
        _eventAppService = GetRequiredService<IEventAppService>();
    }

    [Fact]
    public async Task Should_Get_List_Of_Events()
    {
        //Act
        var result = await _eventAppService.GetListAsync(
            new PagedAndSortedResultRequestDto()
        );

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
        result.Items.ShouldContain(b => b.NameEn == "Test");
    }

    [Fact]
    public async Task Should_Create_A_Valid_Event()
    {
        //Act
        var result = await _eventAppService.CreateAsync(
            new CreateUpdateEventDto
            {
                NameAr = "New test Event 42",
                Capacity = null,
                IsOnline = true,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                IsActive = true,
                OrganizerId = Guid.NewGuid(),
            }
        );

        //Assert
        result.Id.ShouldNotBe(Guid.Empty);
        result.NameEn.ShouldBe("New test Event 42");
    }
    
    [Fact]
    public async Task Should_Not_Create_A_Event_Without_Name()
    {
        var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
        {
            await _eventAppService.CreateAsync(
                new CreateUpdateEventDto
                {
                    NameAr = "New test Event 42",
                    Capacity = null,
                    IsOnline = true,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow,
                    IsActive = true,
                    OrganizerId = Guid.NewGuid(),
                }
            );
        });

        exception.ValidationErrors
            .ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
    }
}