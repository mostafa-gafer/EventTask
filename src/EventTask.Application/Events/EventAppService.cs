using EventTask.Events.Dtos;
using EventTask.Events.Entities;
using EventTask.Events.Interfaces;
using EventTask.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace EventTask.Events;

[Authorize(EventTaskPermissions.Events.Default)]
public class EventAppService : ApplicationService, IEventAppService
{
    private readonly IEventRepository _repository;
    private readonly ICurrentUser _currentUser;
    public EventAppService(IEventRepository repository, ICurrentUser currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<EventDto> GetAsync(Guid id)
    {
        var Event = await _repository.GetAsync(id);
        return ObjectMapper.Map<Event, EventDto>(Event);
    }

    public async Task<PagedResultDto<EventDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "NameAr" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var Events = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<EventDto>(
            totalCount,
            ObjectMapper.Map<List<Event>, List<EventDto>>(Events)
        );
    }

    [Authorize(EventTaskPermissions.Events.Create)]
    public async Task<EventDto> CreateAsync(CreateUpdateEventDto input)
    {
        var eventEntity = new Event(input.NameEn, 
            input.NameAr, 
            input.IsOnline, 
            input.StartDate, 
            input.EndDate, 
            input.OrganizerId, 
            input.Capacity, 
            input.IsActive,
            input.Link,
            input.Location);

        await _repository.InsertAsync(eventEntity);
        return ObjectMapper.Map<Event, EventDto>(eventEntity);
    }

    [Authorize(EventTaskPermissions.Events.Edit)]
    public async Task<EventDto> UpdateAsync(Guid id, CreateUpdateEventDto input)
    {
        var eventEntity = await _repository.GetAsync(id);
        eventEntity.Update(
            input.NameEn,
            input.NameAr,
            input.IsOnline,
            input.StartDate,
            input.EndDate,
            input.OrganizerId,
            input.Capacity,
            input.IsActive,
            input.Link,
            input.Location);

        return ObjectMapper.Map<Event, EventDto>(eventEntity);
    }

    [Authorize(EventTaskPermissions.Events.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}