using EventTask.EventRegistrations;
using EventTask.EventRegistrations.Dtos;
using EventTask.Events.Dtos;
using EventTask.Events.Entities;
using EventTask.Users.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Identity;
using Volo.Abp.Mapperly;

namespace EventTask;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class EventTaskEventToEventDtoMapper : MapperBase<Event, EventDto>
{
    public override partial EventDto Map(Event source);

    public override partial void Map(Event source, EventDto destination);
}

public partial class EventRegistrationToEventRegistrationDtoMapper : MapperBase<EventRegistration, EventRegistrationDto>
{
    public override EventRegistrationDto Map(EventRegistration source)
    {
        var dto = new EventRegistrationDto
        {
            Id = source.Id,
            EventId = source.EventId,
            UserId = source.UserId,
            RegistrationDate = source.RegisteredAt,
            IsCancelled = source.IsCancelled,
            CancellationDate = source.CancellationDate,
            CreationTime = source.CreationTime,
            CreatorId = source.CreatorId,
            LastModificationTime = source.LastModificationTime,
            LastModifierId = source.LastModifierId
        };
        return dto;
    }

    public override void Map(EventRegistration source, EventRegistrationDto destination)
    {
        destination.Id = source.Id;
        destination.EventId = source.EventId;
        destination.UserId = source.UserId;
        destination.RegistrationDate = source.RegisteredAt;
        destination.IsCancelled = source.IsCancelled;
        destination.CancellationDate = source.CancellationDate;
        destination.CreationTime = source.CreationTime;
        destination.CreatorId = source.CreatorId;
        destination.LastModificationTime = source.LastModificationTime;
        destination.LastModifierId = source.LastModifierId;
    }
}

//[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
//public partial class EventTaskCreateUpdateEventDtoToEventMapper : MapperBase<CreateUpdateEventDto, Event>
//{
//    //public override partial Event Map(CreateUpdateEventDto source);

//    public override partial void Map(CreateUpdateEventDto source, Event destination);
//}
