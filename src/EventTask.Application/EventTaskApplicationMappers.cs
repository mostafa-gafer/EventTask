using EventTask.Events.Dtos;
using EventTask.Events.Entities;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EventTask;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class EventTaskEventToEventDtoMapper : MapperBase<Event, EventDto>
{
    public override partial EventDto Map(Event source);

    public override partial void Map(Event source, EventDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class EventTaskCreateUpdateEventDtoToEventMapper : MapperBase<CreateUpdateEventDto, Event>
{
    public override partial Event Map(CreateUpdateEventDto source);

    public override partial void Map(CreateUpdateEventDto source, Event destination);
}
