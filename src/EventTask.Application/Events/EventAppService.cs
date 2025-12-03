using EventTask.Events.Dtos;
using EventTask.Events.Entities;
using EventTask.Events.Interfaces;
using EventTask.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq;

namespace EventTask.Events;

[Authorize(EventTaskPermissions.Events.Default)]
public class EventAppService : ApplicationService, IEventAppService
{
    private readonly IRepository<Event, Guid> _repository;

    public EventAppService(IRepository<Event, Guid> repository)
    {
        _repository = repository;
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
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Name" : input.Sorting)
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
        var Event = ObjectMapper.Map<CreateUpdateEventDto, Event>(input);
        await _repository.InsertAsync(Event);
        return ObjectMapper.Map<Event, EventDto>(Event);
    }

    [Authorize(EventTaskPermissions.Events.Edit)]
    public async Task<EventDto> UpdateAsync(Guid id, CreateUpdateEventDto input)
    {
        var Event = await _repository.GetAsync(id);
        ObjectMapper.Map(input, Event);
        await _repository.UpdateAsync(Event);
        return ObjectMapper.Map<Event, EventDto>(Event);
    }

    [Authorize(EventTaskPermissions.Events.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}